using Application.Models.User;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<UserAppDto> GetAsync(int id);
    Task<IEnumerable<int>> SearchAsync(UserSearchAppDto dto);
    Task<UserAppDto> CreateAsync(UserCreateAppDto dto);
    Task<UserAppDto> UpdateAsync(UserUpdateAppDto dto);
}