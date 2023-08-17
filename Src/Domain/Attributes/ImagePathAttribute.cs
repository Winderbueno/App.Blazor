namespace Domain.Attributes;

public class ImagePathAttribute : Attribute
{
    public string Path { get; private set; }

    public ImagePathAttribute(string path) => Path = path;
}
