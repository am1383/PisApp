namespace PisApp.API.Dtos
{
    public class VIPUserDetailDto
    {   
        public required bool is_VIP    { get; set; }
        public required decimal profit { get; set; }
        public int? remaining_time     { get; set; }
    }
}