﻿using AutoMapper;
using Application.Models.User;
using Domain.Extensions;
using Infrastructure.HttpClients.Shop;
using UserRole = Domain.Enums.User.UserRole; // Todo. Can be optimized ?
using UserStatus = Domain.Enums.User.UserStatus;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class UserProfile : Profile
{
    public UserProfile()
    {
        // App.Blazor -> Shop.Api
        CreateMap<UserCreateFormDto, UserCreateDto>();
        CreateMap<UserSearchFormDto, UserSearchDto>() // Todo. Add Search route with SearchDto in Shop.Api/UserRoutes
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnums<UserStatus>()))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToEnums<UserRole>()));
        CreateMap<UserUpdateFormDto, UserUpdateDto>();

        // Shop.Api -> App.Blazor
        CreateMap<UserDto, UserAppDto>();

        // App.Blazor.Internal
        CreateMap<UserAppDto, UserUpdateFormDto>();
    }
}