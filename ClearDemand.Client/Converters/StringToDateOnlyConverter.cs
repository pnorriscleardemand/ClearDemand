using AutoMapper;

namespace ClearDemand.Client.Converters;

public class StringToDateOnlyConverter : IValueResolver<string, DateOnly, DateOnly>
{
    //public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    //{
    //    return System.Convert.ToDateTime(source);
    //}

    public DateOnly Resolve(string source, DateOnly destination, DateOnly destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source)) return default; // Handle null or empty string as needed

        if (DateOnly.TryParse(source, out var dateOnly)) return dateOnly;

        throw new AutoMapperMappingException($"Unable to convert '{source}' to DateOnly.");
    }
}