using SV_CodingCase.Domain.Models.Common;

namespace SV_CodingCase.Domain.Contract
{
    public static class Errors
    {
        public static Error UnknownError = new Error(9999999, "Uknown Error");
        public static Error RetrievingDataError = new Error(1000001, "Error Retrieving Data");
    }
}
