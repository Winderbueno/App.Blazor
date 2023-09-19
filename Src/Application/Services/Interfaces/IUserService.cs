using Application.Dtos.User;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<UserAppDto> GetAsync(int id);
    Task<IEnumerable<int>> SearchAsync(UserSearchFormDto dto);
    Task<UserAppDto> CreateAsync(UserCreateFormDto dto);
    Task<UserAppDto> UpdateAsync(UserUpdateFormDto dto);
    Task DeleteAsync(int id);
}