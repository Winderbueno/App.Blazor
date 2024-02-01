using Application.Dtos.Dico;

namespace Application.Services.Interfaces;

public interface IDicoService
{
    Task<WordAppDto> GetAsync(int id);
    Task<IEnumerable<WordAppDto>> GetAsync(IEnumerable<int> ids);
    Task<IEnumerable<int>> SearchAsync(WordSearchFormDto dto);
}