using SV_CodingCase.Domain.Models;

namespace SV_CodingCase.Domain.Contract
{
    public class LockSearchResultDto
    {
        public int Weight { get; set; }
        public Lock Lock { get; set; }
    }
}
