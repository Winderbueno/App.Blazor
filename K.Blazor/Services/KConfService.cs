namespace K.Blazor.Services;

public class KConfService
{
    private Func<object?, string?> name = x => x?.ToString();

    public Func<object?, string?> DefaultName => name;

    public void SetDefaultNameGetter(Func<object?, string?> name) => this.name = name;
}