namespace Ui.Shared.Containers.Form;

public class RadioItem<T>
{
    public string? DisplayName { get; set; }

    public T? Value { get; set; }

    public string? ImagePath { get; set; }

    public string? Info { get; set; }
}