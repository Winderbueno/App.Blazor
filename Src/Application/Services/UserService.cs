using AutoMapper;
using Application.Models.User;
using Application.Services.Interfaces;
using Infrastructure.HttpClients.Shop;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IShopApi _shopApi;

    public UserService(
        IMapper mapper,
        IShopApi shopApi)
    {
        _mapper = mapper;
        _shopApi = shopApi;
    }

    public async Task<UserAppDto> GetAsync(int id)
        => _mapper.Map<UserAppDto>(await _shopApi.UserGETAsync(id));

    public async Task<IEnumerable<AccountResponse>> SearchAsync(UserSearchAppDto dto)
        => await _shopApi.UserAllAsync();

    public async Task<UserAppDto> CreateAsync(UserCreateAppDto dto)
        => _mapper.Map<UserAppDto>(
            await _shopApi.CreateAsync(_mapper.Map<CreateAccountRequest>(dto)));

    public async Task<UserAppDto> UpdateAsync(UserUpdateAppDto dto)
        => _mapper.Map<UserAppDto>(
            await _shopApi.UserPUTAsync(dto.UserId, _mapper.Map<UpdateAccountRequest>(dto)));
}