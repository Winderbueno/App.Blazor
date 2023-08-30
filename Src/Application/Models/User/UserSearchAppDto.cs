namespace Application.Models.User;

public class UserSearchAppDto
{
    public string? Contains { get; set; }

    public List<string>? Status { get; set; }
    public List<string>? Types { get; set; }
    public List<string>? Functions { get; set; }

    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}