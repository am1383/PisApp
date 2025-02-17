namespace PisApp.API.Dtos
{
    public class ResponseDto<T>
    {
        public bool IsSuccess  { get; set; }

        public string? Message { get; set; }

        public T Data          { get; set; }

        public ResponseDto(bool isSuccess, T data, string? message = "") 
        { 
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
