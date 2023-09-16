using Domain.Attributes;
using K.Blazor.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Extensions;

public static class AnnotationExtensions
{
    public static string? DisplayName<T>(this T? value)
        => GetFieldAttribute<T?, DisplayAttribute>(value)?.Name ?? value?.ToString();

    // https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
    public static Color? Color<T>(this T? value) where T : struct, IConvertible
        => GetFieldAttribute<T?, ColorAttribute>(value)?.Color ?? null;

    private static TAttribute? GetFieldAttribute<T, TAttribute>(T value) where TAttribute : Attribute
        => value?.GetType()
            .GetField(value.ToString()!)?
            .GetCustomAttributes<TAttribute>()
            .FirstOrDefault();
}
