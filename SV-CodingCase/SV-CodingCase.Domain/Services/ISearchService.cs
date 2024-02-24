
using SV_CodingCase.Domain.Contract;

namespace SV_CodingCase.Domain.Services
{
    public interface ISearchService
    {
        Task<SearchResultDto> Search(string searchInput);
    }
}
