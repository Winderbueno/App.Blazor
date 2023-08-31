using K.Blazor.Dtos;

namespace K.Blazor.Services;

public class KConfService
{
    private SelectFieldOptions selectFieldOptions = new();

    public SelectFieldOptions SelectFieldOptions => selectFieldOptions;

    public void SetSelectFieldOptions(SelectFieldOptions options)
        => selectFieldOptions = options;
}