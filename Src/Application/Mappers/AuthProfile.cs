using AutoMapper;
using Application.Dtos.Auth;
using Infrastructure.HttpClients.Shop;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class AuthProfile : Profile
{
    public AuthProfile()
    {
        // Shop.Api -> App.Blazor
        CreateMap<AuthUserDto, User>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.JwtToken))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<string>() { src.Role }));
    }
}