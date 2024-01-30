namespace Application.Dtos.Dico;

public class WordSearchFormDto
{
    public string? Contains { get; set; }

    public int? Hsk { get; set; }
    public List<string>? Domains { get; set; }
    public List<string>? Types { get; set; }

    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}