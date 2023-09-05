namespace K.Blazor.Configuration;

public class KConfService
{
    private SelectFieldOptions selectFieldOptions = new();

    public SelectFieldOptions SelectFieldOptions => selectFieldOptions;

    public void SetSelectFieldOptions(SelectFieldOptions opts) => selectFieldOptions = opts;
}