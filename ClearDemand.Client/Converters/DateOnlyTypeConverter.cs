using AutoMapper;

namespace ClearDemand.Client.Converters;

public class DateOnlyTypeConverter : ITypeConverter<string, DateOnly>
{
    public DateOnly Convert(string source, DateOnly destination, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source)) return default; // Handle null or empty string as needed

        if (DateOnly.TryParse(source, out var dateOnly)) return dateOnly;

        throw new AutoMapperMappingException($"Unable to convert '{source}' to DateOnly.");
    }
}