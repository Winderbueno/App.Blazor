using AutoMapper;
using Application.Dtos.Dico;
using Domain.Extensions;
using Infrastructure.HttpClients.Shop;
using WordDomain = Domain.Enums.Dico.WordDomain; // Todo. Can be optimized ?
using WordType = Domain.Enums.Dico.WordType;

namespace Application.Mappers;

// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
public class DicoProfile : Profile
{
    public DicoProfile()
    {
        // App -> Api
        CreateMap<WordSearchFormDto, WordSearchDto>()
            .ForMember(dest => dest.Domains, opt => opt.MapFrom(src => src.Domains.ToEnums<WordDomain>()))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.ToEnums<WordType>()));

        // Api -> App
        CreateMap<WordDto, WordAppDto>();
    }
}