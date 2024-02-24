using SV_CodingCase.Domain.Repositories;

namespace SV_CodingCase.Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDataRepository _dataRepository;
        public SearchService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
    }
}
