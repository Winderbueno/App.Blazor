using Application.Models.User;
using Infrastructure.HttpClients.Shop;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<UserAppDto> GetAsync(int id);
    Task<IEnumerable<AccountResponse>> SearchAsync(UserSearchAppDto dto);
    Task<UserAppDto> CreateAsync(UserCreateAppDto dto);
    Task<UserAppDto> UpdateAsync(UserUpdateAppDto dto);
}