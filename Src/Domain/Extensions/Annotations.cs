using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Extensions;

public static class AnnotationExtensions
{
    // Return Name property of DisplayAttribute
    public static string? DisplayName<T>(this T value)
        => GetFieldInfo(value)?
            .GetCustomAttributes<DisplayAttribute>()
            .FirstOrDefault()?.Name ?? value?.ToString();

    private static FieldInfo? GetFieldInfo<T>(T value)
        => value?.GetType().GetField(value.ToString()!);
}
