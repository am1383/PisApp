namespace PisApp.API.DTOs
{
    public class VIPUserDetailDto
    {   
        public required bool is_VIP { get; set; }
        public int? remaining_time { get; set; }
    }
}