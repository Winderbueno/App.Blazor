using Application.Dtos.Auth;
using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.HttpClients.Shop;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IShopApi _shopApi;

    public AuthService(
        IMapper mapper,
        IShopApi shopApi)
    {
        _mapper = mapper;
        _shopApi = shopApi;
    }

    public async Task<User?> SignInAsync(string username, string pwd)
        => _mapper.Map<User>(await _shopApi.AuthSignInAsync(new() { Email = username, Password = pwd }));

    public async Task<User?> RefreshTokenAsync()
        => _mapper.Map<User>(await _shopApi.AuthRefreshTokenAsync());

    public async Task RevokeRefreshTokenAsync()
        => await _shopApi.AuthRevokeTokenAsync(new());
}