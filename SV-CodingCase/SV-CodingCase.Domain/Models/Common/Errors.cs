namespace SV_CodingCase.Domain.Models.Common
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = default!;
        public Error(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
