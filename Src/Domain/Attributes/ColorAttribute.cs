using K.Blazor.Enums;

namespace Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ColorAttribute : Attribute
{
    public Color? Color { get; private set; }

    public ColorAttribute(Color color) => Color = color;
}
