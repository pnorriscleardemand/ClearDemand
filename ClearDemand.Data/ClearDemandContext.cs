using ClearDemand.Models.EntityFrameworkModels;
using Microsoft.EntityFrameworkCore;

namespace ClearDemand.Data;

public partial class ClearDemandContext : DbContext
{
    public ClearDemandContext()
    {
    }

    public ClearDemandContext(DbContextOptions<ClearDemandContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Markdown> Markdowns { get; set; }

    public virtual DbSet<MarkdownPlan> MarkdownPlans { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("categoryid");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.Property(e => e.InventoryId).HasColumnName("inventoryid");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantityinstock");
            entity.Property(e => e.ReorderLevel).HasColumnName("reorderlevel");
            entity.Property(e => e.SupplierId).HasColumnName("supplierid");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("inventory_productid_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("inventory_supplierid_fkey");
        });

        modelBuilder.Entity<Markdown>(entity =>
        {
            entity.HasKey(e => e.MarkdownId).HasName("markdowns_pkey");

            entity.ToTable("markdowns");

            entity.HasIndex(e => e.MarkdownPlanId, "fki_markdowns_markdownplan_fkey");

            entity.Property(e => e.MarkdownId).HasColumnName("markdownid");
            entity.Property(e => e.EndDate).HasColumnName("enddate");
            entity.Property(e => e.MarkdownPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("markdownpercentage");
            entity.Property(e => e.MarkdownPlanId).HasColumnName("markdownplanid");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.StartDate).HasColumnName("startdate");

            entity.HasOne(d => d.MarkdownPlan).WithMany(p => p.Markdowns)
                .HasForeignKey(d => d.MarkdownPlanId)
                .HasConstraintName("markdowns_markdownplan_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Markdowns)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("markdowns_productid_fkey");
        });

        modelBuilder.Entity<MarkdownPlan>(entity =>
        {
            entity.HasKey(e => e.MarkdownPlanId).HasName("markdownplans_pkey");

            entity.ToTable("markdownplans");

            entity.Property(e => e.MarkdownPlanId).HasColumnName("markdownplanid");
            entity.Property(e => e.EndDate).HasColumnName("enddate");
            entity.Property(e => e.MarkdownPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("markdownpercentage");
            entity.Property(e => e.PlanDescription).HasColumnName("plandescription");
            entity.Property(e => e.PlanName)
                .HasMaxLength(255)
                .HasColumnName("planname");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.StartDate).HasColumnName("startdate");

            entity.HasOne(d => d.Product).WithMany(p => p.MarkdownPlans)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("markdownplans_productid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.CategoryId).HasColumnName("categoryid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.UPC).HasColumnName("upc");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantityinstock");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("productname");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("products_categoryid_fkey");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("sales_pkey");

            entity.ToTable("sales");

            entity.Property(e => e.SaleId).HasColumnName("saleid");
            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.SaleDate).HasColumnName("saledate");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("sales_customerid_fkey");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(e => e.SaleItemId).HasName("saleitems_pkey");

            entity.ToTable("saleitems");

            entity.Property(e => e.SaleItemId).HasColumnName("saleitemid");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.QuantitySold).HasColumnName("quantitysold");
            entity.Property(e => e.SaleId).HasColumnName("saleid");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");

            entity.Property(e => e.MarkdownPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("markdownpercentage");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("saleitems_productid_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("saleitems_saleid_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("supplierid");
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("contactname");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .HasColumnName("suppliername");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}