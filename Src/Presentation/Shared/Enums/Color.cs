namespace Presentation.Shared.Enums;

public enum Color
{
    Primary,
    Accent,
    Info,
    Success,
    Warning,
    Danger,
    Light,
    Dark,
    Disabled
}

public static class ColorExtensions
{
    public static string ToCssVar(this Color? color)
        => $"var(--k-color-{(color == null ? "info" : color.ToString()!.ToLower())})";
}