namespace CleaningSaboms.Results
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public ErrorType? Type { get; set; }

        public static ServiceResult Ok(string? message = null)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message
            };
        }

        public static ServiceResult Fail(string message, ErrorType errorType = ErrorType.UnexpectedError)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                Type = errorType
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        public static new ServiceResult<T> Ok(T data, string? message = null)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static new ServiceResult<T> Fail(string message, ErrorType errorType = ErrorType.UnexpectedError)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Type = errorType
            };
        }
    }
}
