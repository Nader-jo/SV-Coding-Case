using MediatR;
using SV_CodingCase.Domain.Contract;
using SV_CodingCase.Domain.Models.Common;
using SV_CodingCase.Domain.Services;

namespace SV_CodingCase.Domain.Application
{
    public class SearchRequest
    {
        public record Request(string SearchInput) : IRequest<Result<SearchResultDto>>;
        public class SearchRequestHandler : IRequestHandler<Request, Result<SearchResultDto>>
        {
            private readonly ISearchService _searchService;

            public SearchRequestHandler(ISearchService searchService)
            {
                _searchService = searchService;
            }

            public async Task<Result<SearchResultDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var searchResult = await _searchService.Search(request.SearchInput);
                return Result<SearchResultDto>.Success(searchResult);
            }
        }
    }
}
