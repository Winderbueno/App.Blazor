namespace Application.Models.User;

public class UserSearchFormDto
{
    public string? Contains { get; set; }

    public List<string>? Status { get; set; }
    public List<string>? Roles { get; set; }

    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}