using Domain.Enums.Dico;

namespace Application.Dtos.Dico;

public class WordAppDto
{
    public int WordId { get; set; }
    public int ChineseWordId { get; set; }
    public WordDomain? Domain { get; set; }
    public WordType Type { get; set; }
    public int? Hsk { get; set; }
    public string? Word { get; set; }
    public string? Pinyin { get; set; }
    public string? PinyinGen { get; set; }
    public string? English { get; set; }
}