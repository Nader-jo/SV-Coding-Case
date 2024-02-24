namespace SV_CodingCase.Extensions.Common
{
    public struct ResultDto
    {
        public string Status { get; init; }
        public string ErrorMessage { get; init; }

        public static ResultDto Ok() => new() { Status = ResultStatus.Success };

        public static ResultDto Fail(string errorMessage) => new()
        { Status = ResultStatus.Failure, ErrorMessage = errorMessage };
    }

    public struct ResultDto<T>
    {
        public string Status { get; init; }
        public T Result { get; init; }
        public string ErrorMessage { get; init; }

        public static ResultDto<T> Ok(T result) => new() { Status = ResultStatus.Success, Result = result };

        public static ResultDto<T> Fail(string errorMessage) => new()
        { Status = ResultStatus.Failure, ErrorMessage = errorMessage };
    }
}