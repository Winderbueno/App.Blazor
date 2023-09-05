namespace K.Blazor.Configuration;

public class SelectFieldOptions
{
    public Func<string> RequiredMessage { get; set; } = () => "Field is required";
    public Func<string> NoResultMessage { get; set; } = () => "No result";
    public Func<object?, string?> DefaultOptionName { get; set; } = x => x?.ToString();
}
