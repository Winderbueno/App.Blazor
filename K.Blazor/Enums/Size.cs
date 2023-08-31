namespace K.Blazor.Enums;

public enum Size
{
    Smallest,
    Smaller,
    Small,
    Normal,
    Large,
    Larger,
    Largest
}

public static class SizeExtensions
{
    public static string ToCssVar(this Size? size)
        => $"var(--k-gap-{(size == null ? "normal" : size.ToString()!.ToLower())})";
}