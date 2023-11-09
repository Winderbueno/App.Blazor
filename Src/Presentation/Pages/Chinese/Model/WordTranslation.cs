namespace Presentation.Pages.Chinese.Model;

public class WordTranslation
{
    public string? English { get; set; }
    public string? Pinyin { get; set; }
    public bool SeparableVerb { get; set; } = false;
    public string? Comment { get; set; }
}