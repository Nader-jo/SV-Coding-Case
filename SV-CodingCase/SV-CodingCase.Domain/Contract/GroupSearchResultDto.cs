using SV_CodingCase.Domain.Models;

namespace SV_CodingCase.Domain.Contract
{
    public class GroupSearchResultDto
    {
        public int Weight { get; set; }
        public Group Group { get; set; }
    }
}
