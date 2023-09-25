﻿using AutoMapper;

namespace ClearDemand.Client.Converters;

public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
{
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        return System.Convert.ToDateTime(source);
    }
}