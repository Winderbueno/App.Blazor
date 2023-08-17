using Domain.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Enums;

public static class EnumHelper
{
    public static string? GetName<T>(this T value)
    {
        var fieldInfo = GetFieldInfo(value);
        return fieldInfo?.GetCustomAttributes<DisplayAttribute>().FirstOrDefault()?.Name;
    }

    public static string? GetImagePath<T>(this T value)
    {
        var fieldInfo = GetFieldInfo(value);
        return fieldInfo?.GetCustomAttributes<ImagePathAttribute>().FirstOrDefault()?.Path;
    }

    public static string? GetInfo<T>(this T value)
    {
        var fieldInfo = GetFieldInfo(value);
        return fieldInfo?.GetCustomAttributes<InfoAttribute>().FirstOrDefault()?.Text;
    }

    private static FieldInfo? GetFieldInfo<T>(T value) => value?.GetType().GetField(value.ToString());
}
