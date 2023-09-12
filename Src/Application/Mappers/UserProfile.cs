﻿using AutoMapper;
using Application.Models.User;
using Domain.Enums.User;
using Domain.Extensions;
using Infrastructure.HttpClients.Shop;
using UserRole = Domain.Enums.User.UserRole;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class UserProfile : Profile
{
    public UserProfile()
    {
        // App.Blazor -> Shop.Api
        CreateMap<UserCreateAppDto, UserCreateDto>();
        CreateMap<UserSearchAppDto, UserSearchAppDto>() // Todo. Add Search route with SearchDto in Shop.Api/UserRoutes
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnums<UserStatus>()))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToEnums<UserRole>()));
        CreateMap<UserUpdateAppDto, UserUpdateDto>();

        // Shop.Api -> App.Blazor
        CreateMap<UserDto, UserAppDto>();

        // App.Blazor.Internal
        CreateMap<UserAppDto, UserUpdateAppDto>();
    }
}