using AutoMapper;
using Application.Dtos.User;
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
        => _mapper.Map<UserAppDto>(await _shopApi.UserGetAsync(id));

    public async Task<IEnumerable<int>> SearchAsync(UserSearchFormDto dto)
        => await _shopApi.UserSearchAsync(_mapper.Map<UserSearchDto>(dto));

    public async Task<UserAppDto> CreateAsync(UserCreateFormDto dto)
        => _mapper.Map<UserAppDto>(await _shopApi.UserCreateAsync(_mapper.Map<UserCreateDto>(dto)));

    public async Task<UserAppDto> UpdateAsync(UserUpdateFormDto dto)
        => _mapper.Map<UserAppDto>(await _shopApi.UserUpdateAsync(_mapper.Map<UserUpdateDto>(dto)));

    public async Task DeleteAsync(int id)
        => await _shopApi.UserDeleteAsync(id);
}