using ClearDemand.Business.Contracts;
using ClearDemand.Data;
using ClearDemand.Models.EntityFrameworkModels;
using Microsoft.EntityFrameworkCore;

namespace ClearDemand.Business.Services;

public class MarkdownPlanService : IMarkdownPlanService
{
    private readonly ClearDemandContext _context; // database context

    public MarkdownPlanService(
        ClearDemandContext context
    )
    {
        _context = context;
    }

    // get a list of all markdown plans
    public async Task<IEnumerable<MarkdownPlan>> Get()
    {
        var markdownPlans = await _context.MarkdownPlans
            //.Include(b => b.Markdown)
            .ToListAsync();
        return markdownPlans;
    }

    // get a single markdown plan
    public async Task<MarkdownPlan?> Get(int id)
    {
        var markdownPlan = await _context.MarkdownPlans
            .Include(b => b.Markdowns)
            .FirstOrDefaultAsync(x => x.MarkdownPlanId == id);

        return markdownPlan;
    }

    // get a list of all markdown plans by product
    public async Task<IEnumerable<MarkdownPlan>> GetByProduct(int? productId)
    {
        var markdownPlans = await _context.MarkdownPlans
            .Where(w => w.ProductId == productId)
            .Include(b => b.Markdowns!.OrderBy(o => o.StartDate))
            .ToListAsync();

        return markdownPlans;
    }

    // get a list of all markdown plans by product and date
    public async Task<MarkdownPlan> GetByProductAndDate(int? productId, DateOnly? date)
    {
        var markdownPlans = await _context.MarkdownPlans
            .Where(w => w.ProductId == productId)
            .Where(e => e.StartDate <= date)
            .Where(e => e.EndDate >= date)
            .Include(b => b.Markdowns)
            .Include(b => b.Product)
            .ToListAsync();
        return markdownPlans.FirstOrDefault()!;
    }

    // create a new markdown plan
    public async Task<MarkdownPlan> Create(MarkdownPlan markdownPlan)
    {
        ValidateModel(markdownPlan);

        _context.Add(markdownPlan);
        await _context.SaveChangesAsync();

        return markdownPlan;
    }


    // update a markdown plan
    public async Task<MarkdownPlan> Update(MarkdownPlan markdownPlan)
    {
        var isValid = ValidateModel(markdownPlan);
        if (isValid == false) throw new Exception("Invalid markdown plan.");

        var existingPlan =
            await _context.MarkdownPlans
                .Include(i => i.Markdowns)
                .FirstOrDefaultAsync(m => m.MarkdownPlanId == markdownPlan.MarkdownPlanId);

        if (existingPlan != null)
        {
            UpdatePlan(markdownPlan, existingPlan, _context);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        return markdownPlan;
    }


    // delete a markdown
    public bool Delete(int id)
    {
        var markdownPlan = _context.MarkdownPlans.FirstOrDefault(x => x.MarkdownPlanId == id);

        if (markdownPlan != null)
        {
            _context.Remove(markdownPlan);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    private static void UpdatePlan(MarkdownPlan markdownPlan, MarkdownPlan existingPlan,
        ClearDemandContext context)
    {
        context.Entry(existingPlan).CurrentValues.SetValues(markdownPlan);

        // Update, add, or remove children as needed
        // For example, updating children:
        foreach (var incomingChild in markdownPlan.Markdowns!)
        {
            var existingMarkdown =
                existingPlan.Markdowns!.FirstOrDefault(c => c.MarkdownId == incomingChild.MarkdownId);
            if (existingMarkdown != null)
            {
                context.Entry(existingMarkdown).CurrentValues.SetValues(incomingChild);
            }
            else
            {
                // Add new child if it doesn't exist
                existingPlan.Markdowns!.Add(incomingChild);
            }
        }

        // Remove children that are no longer associated
        var childrenToRemove = existingPlan.Markdowns!
            .Where(c => markdownPlan.Markdowns.All(ic => ic.MarkdownId != c.MarkdownId)).ToList();
        foreach (var childToRemove in childrenToRemove) existingPlan.Markdowns!.Remove(childToRemove);
    }

    private bool ValidateModel(MarkdownPlan markdownPlan)
    {
        var isOverlapping = AreDateRangesOverlapping(markdownPlan.Markdowns);
        if (isOverlapping) return false;

        var isContiguous = AreDateRangesContiguous(markdownPlan.Markdowns);
        if (!isContiguous) return false;

        return true;
    }

    public static bool AreDateRangesOverlapping(List<Markdown>? markdowns)
    {
        if (markdowns is not { Count: > 1 })
            // No overlaps if there are fewer than two ranges.
            return false;

        // Sort the date ranges by their start date.
        var sortedMarkdowns = markdowns.OrderBy(o => o.StartDate).ToList();

        for (var i = 0; i < sortedMarkdowns.Count - 1; i++)
            if (markdowns[i].EndDate >= markdowns[i + 1].StartDate)
                // Overlap detected.
                return true;

        // No overlaps found.
        return false;
    }

    public static bool AreDateRangesContiguous(List<Markdown>? markdowns)
    {
        if (markdowns is not { Count: > 1 })
            // No gaps if there are fewer than two ranges.
            return true;

        // Sort the date ranges by their start date.
        var sortedMarkdowns = markdowns.OrderBy(o => o.StartDate).ToList();

        for (var i = 0; i < sortedMarkdowns.Count - 1; i++)
            if (sortedMarkdowns[i].EndDate?.AddDays(1) < sortedMarkdowns[i + 1].StartDate)
                // Gap detected.
                return false;

        // No gaps found.
        return true;
    }
}