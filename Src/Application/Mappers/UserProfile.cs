using AutoMapper;
using Application.Models.User;
using Domain.Enums.User;
using Infrastructure.HttpClients.Shop;
using Domain.Extensions;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class UserProfile : Profile
{
    public UserProfile()
    {
        // App.Blazor -> Shop.Api
        CreateMap<UserCreateAppDto, CreateAccountRequest>();
        CreateMap<UserSearchAppDto, UserSearchAppDto>() // Todo. Add Search route with SearchDto in Shop.Api/UserRoutes
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnums<UserStatus>()))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToEnums<UserType>()))
            .ForMember(dest => dest.Functions, opt => opt.MapFrom(src => src.Functions.ToEnums<UserFunction>()));
        CreateMap<UserUpdateAppDto, UpdateAccountRequest>();

        // Shop.Api -> App.Blazor
        CreateMap<AccountResponse, UserAppDto>();
    }
}