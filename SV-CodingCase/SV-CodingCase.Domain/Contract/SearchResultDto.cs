namespace SV_CodingCase.Domain.Contract
{
    public class SearchResultDto
    {
        public List<BuidingSearchResultDto> Buildings { get; set; }
        public List<LockSearchResultDto> Locks { get; set; }
        public List<GroupSearchResultDto> Groups { get; set; }
        public List<MediumSearchResultDto> Medium { get; set; }
    }
}
