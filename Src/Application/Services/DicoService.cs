using AutoMapper;
using Application.Services.Interfaces;
using Infrastructure.HttpClients.Shop;
using Application.Dtos.Dico;

namespace Application.Services;

public class DicoService : IDicoService
{
    private readonly IMapper _mapper;
    private readonly IShopApi _shopApi;

    public DicoService(
        IMapper mapper,
        IShopApi shopApi)
    {
        _mapper = mapper;
        _shopApi = shopApi;
    }

    public async Task<WordAppDto> GetAsync(int id)
        => _mapper.Map<WordAppDto>(await _shopApi.DicoGetAsync(id));

    public async Task<IEnumerable<WordAppDto>> GetAsync(IEnumerable<int> ids)
        => _mapper.Map<IEnumerable<WordAppDto>>(await _shopApi.DicoGetAllAsync(ids));

    public async Task<IEnumerable<int>> SearchAsync(WordSearchFormDto dto)
        => await _shopApi.DicoSearchAsync(_mapper.Map<WordSearchDto>(dto));
}