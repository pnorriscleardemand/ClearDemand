using System.Reflection;
using AutoMapper;

namespace ClearDemand.Client.Converters;

public class TypeTypeConverter
{
    public Type? Convert(string source, Type destination, ResolutionContext context)
    {
        return Assembly.GetExecutingAssembly().GetType(source);
    }
}