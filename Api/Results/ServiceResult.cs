namespace CleaningSaboms.Results
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        
        public static ServiceResult Ok(string? message = null)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message
            };
        }

        public static ServiceResult Fail(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message
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
        public static new ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
