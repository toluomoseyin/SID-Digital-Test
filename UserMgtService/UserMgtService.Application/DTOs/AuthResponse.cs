namespace UserMgtService.Application.DTOs
{
    public class AuthResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static AuthResponse Fail(string message)
        {
            return new AuthResponse
            {
                Success = false,
                Message = message,
            };
        }

        public static AuthResponse Successful(string message)
        {
            return new AuthResponse
            {
                Success = true,
                Message = message,
            };
        }
    }
    public class AuthResponse<T> : AuthResponse
    {

        public T Data { get; set; }

        public static AuthResponse<T> Failure(string message, T? data = default)
        {
            return new AuthResponse<T>
            {
                Data = data,
                Success = false,
                Message = message,
            };
        }

        public static AuthResponse<T> Successful(string message, T? data = default)
        {
            return new AuthResponse<T>
            {
                Data = data,
                Success = true,
                Message = message,
            };
        }
    }
}
