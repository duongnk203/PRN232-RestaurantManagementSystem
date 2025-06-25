namespace RestaurantSystem.DTOs
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ResponseModel()
        {
            Errors = new List<string>();
        }

        public static ResponseModel<T> Success(T data, string message = "Success")
        {
            return new ResponseModel<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseModel<T> Fail(string message, List<string> errors = null)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}