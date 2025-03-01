namespace PisApp.API.Dtos
{
    public class ResponseDto<T>
    {
        public string? Message { get; set; }

        public T Data          { get; set; }

        public ResponseDto(T data, string message = "") 
        {  
            Message = message;
            Data    = data;
        }
    }
}
