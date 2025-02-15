namespace PisApp.API.Dtos
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; }

        public string? DeveloperMessage { get; set; }

        public string? Message { get; set; }

        public T Data { get; set; }

        public ResponseDto(bool isSuccess, T data, string? message = null, string? developerMessage = null) 
        { 
            IsSuccess = isSuccess;
            Message = message;
            DeveloperMessage = developerMessage;
            Data = data;
        }
    }
}
