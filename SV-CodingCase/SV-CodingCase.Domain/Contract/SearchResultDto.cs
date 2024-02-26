namespace SV_CodingCase.Domain.Contract
{
    public class SearchResultDto
    {
        public List<BuidingSearchResultDto> Buildings { get; set; } = default!;
        public List<LockSearchResultDto> Locks { get; set; } = default!;
        public List<GroupSearchResultDto> Groups { get; set; } = default!;
        public List<MediumSearchResultDto> Medium { get; set; } = default!;
    }
}
