using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class User
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();

    public ClaimsPrincipal ToClaimsPrincipal()
        => new(new ClaimsIdentity(
            new Claim[] { new (ClaimTypes.Name, Username) }
                .Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()), "Jwt"));

    public static User FromClaimsPrincipal(ClaimsPrincipal principal)
        => new()
        {
            Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };
}