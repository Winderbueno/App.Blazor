using Domain.Extensions;
using Microsoft.CodeAnalysis;

namespace Ui.Shared.Containers.Form;

public static class OptionHelper
{
    // TData can be either a string, an Enum or an Object
    public static IEnumerable<Option> ToOption<TData>(this IEnumerable<TData?> datas,
        Func<TData?, string?>? name = null,
        Func<TData?, string?>? value = null)
            => datas.Select(data => new Option
            {
                Name = name != null ? name(data) : data?.DisplayName(),
                Value = value != null ? value(data) : data?.ToString()
            }).OrderBy(x => x.Name);
}