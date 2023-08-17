using Domain.Enums;
using Microsoft.CodeAnalysis;

namespace Ui.Shared.Containers.Form;

public static class SelectorHelper
{
    public static IEnumerable<OptionItem> ToOptionItem(this IEnumerable<string?> items)
        => items.Select(item => new OptionItem { Name = item, Value = item }).OrderBy(x => x.Name);

    public static IEnumerable<OptionItem> ToOptionItem<TEnum>(this IEnumerable<TEnum> items)
        => items.Select(item => new OptionItem
        {
            Name = item.GetName() ?? item?.ToString(),
            Value = item?.ToString()
        }).OrderBy(x => x.Name);

    public static IEnumerable<OptionItem> ToOptionItem<TObject>(this IEnumerable<TObject?> items,
        Func<TObject?, string?> value,
        Func<TObject?, string?> name)
            => items.Select(item => new OptionItem
            {
                Name = name(item),
                Value = value(item)
            }).OrderBy(x => x.Name);

    public static IEnumerable<RadioItem<T>> ToRadioItem<T>(this IEnumerable<T> items)
        => items.Select(item => new RadioItem<T>
        {
            DisplayName = item.GetName(),
            Value = item,
            ImagePath = item.GetImagePath(),
            Info = item.GetInfo()
        });

    public static IEnumerable<RadioItem<string>> ToRadioItem(this IEnumerable<string> items)
        => items.Select(item => new RadioItem<string>
        {
            DisplayName = item,
            Value = item
        });
}