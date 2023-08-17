namespace Domain.Extensions;

public static class StringExtensions
{
    public static T?[]? ToEnums<T>(this List<string>? values)
        => values?.Select(x => (T?)Enum.Parse(typeof(T?), x)).ToArray();
}
