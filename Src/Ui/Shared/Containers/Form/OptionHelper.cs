using Domain.Extensions;
using Microsoft.CodeAnalysis;

namespace Ui.Shared.Containers.Form;

public static class OptionHelper
{
    public static IEnumerable<Option> ToOption(this IEnumerable<string?> items)
        => items.Select(item => new Option { Name = item, Value = item }).OrderBy(x => x.Name);

    public static IEnumerable<Option> ToOption<TEnum>(this IEnumerable<TEnum> items)
        => items.Select(item => new Option
        {
            Name = item.DisplayName(),
            Value = item?.ToString()
        }).OrderBy(x => x.Name);

    public static IEnumerable<Option> ToOption<TObject>(this IEnumerable<TObject?> items,
        Func<TObject?, string?> value,
        Func<TObject?, string?> name)
            => items.Select(item => new Option
            {
                Name = name(item),
                Value = value(item)
            }).OrderBy(x => x.Name);
}