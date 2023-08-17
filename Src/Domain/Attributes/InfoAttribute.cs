namespace Domain.Attributes;

public class InfoAttribute : Attribute
{
    public string Text { get; private set; }

    public InfoAttribute(string text) => Text = text;
}
