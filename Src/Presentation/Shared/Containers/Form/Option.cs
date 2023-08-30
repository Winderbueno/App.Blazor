using Domain.Extensions;

namespace Presentation.Shared.Containers.Form;

public class Option
{
    public string? Name { get; set; }

    public string? Value { get; set; }
}

public static class OptionExtensions
{
    // Convert a variable of any type in an Option
    // TData can be either a string, an Enum or any Object
    public static IEnumerable<Option> ToOption<TData>(this IEnumerable<TData?> datas,
        Func<TData?, string?>? name = null,
        Func<TData?, string?>? value = null)
        => datas is IEnumerable<Option> options ?
            options
            : datas.Select(data => new Option
            {
                Name = name != null ? name(data) : data?.DisplayName(),
                Value = value != null ? value(data) : data?.ToString()
            }).OrderBy(x => x.Name);
}