using System.Net;

namespace RestaurantSystem.DTOs
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public List<string> Errors { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        private ServiceResult()
        {
            Errors = new List<string>();
        }

        public static ServiceResult<T> Success(T data, string message = "Success")
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static ServiceResult<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ServiceResult<T> NotFound(string message = "Resource not found")
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public static ServiceResult<T> Error(string message, List<string> errors = null)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}