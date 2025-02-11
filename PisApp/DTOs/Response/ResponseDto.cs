namespace PisApp.API.DTOs
{
    public class ResponseDTO<T>
    {
        public bool IsSuccess { get; set; }
        public string? DeveloperMessage { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ResponseDTO(bool isSuccess, string message, string? developerMessage, T? data) 
        { 
            IsSuccess = isSuccess;
            Message = message;
            DeveloperMessage = developerMessage;
            Data = data;
        }
    }
}
