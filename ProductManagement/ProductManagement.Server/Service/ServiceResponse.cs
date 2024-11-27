namespace ProductManagement.Server.Service
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResponse<T> Success(T data) => new() { 
            IsSuccess = true,
            Data = data
        };
        public static ServiceResponse<T> Failure(string message) => new() { 
            IsSuccess = false,
            Message = message
        };
    }
}
