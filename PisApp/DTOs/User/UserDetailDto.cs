using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PisApp.API.DTOs
{
    public class UserDetailDto
    {
        public required string first_name { get; set; }
        public required string last_name { get; set; }
        public required decimal wallet_balance { get; set; }

        [Key]
        public required string referral_code { get; set; }
        public required DateTime time_stamp { get; set; }
        
        [NotMapped]
        public VIPUserDetailDto? vip_detail { get; set; }
    }
}