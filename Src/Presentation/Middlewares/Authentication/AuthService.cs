using Infrastructure.HttpClients.Shop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthService
{
    private readonly AuthStorage _authStorage;
    private readonly IShopApi _shopApi;

    public AuthService(
        AuthStorage authStorage,
        IShopApi shopApi)
    {
        _authStorage = authStorage;
        _shopApi = shopApi;
    }

    public async Task<User> SignInAsync(string username, string password)
    {
        var resp = await _shopApi.AuthenticateAsync(new () { 
            Email = username, 
            Password = password 
        });

        _authStorage.Token = resp.JwtToken;

        return new()
        {
            Username = resp.UserName,
            Password = password,
            Roles = new() {  resp.Role },
        };
    }

    public User? FetchUserFromBrowser()
    {
        //var claimsPrincipal = CreateClaimsPrincipalFromToken(_authStorage.Token);
        //return _authStorage.Token == "" ? null : new() {Username="Kevin"};
        return new() {
            Username="kevin.gellenoncourt@gmail.com",
            Password="patate" 
        };
    }

    public void ClearBrowserUserData() => _authStorage.Token = "";

    private ClaimsPrincipal? CreateClaimsPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var identity = new ClaimsIdentity();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            identity = new(jwtSecurityToken.Claims, "Jwt");
        }

        return token == "" ? null : new(identity);
    }
}