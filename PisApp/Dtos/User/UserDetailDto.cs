namespace PisApp.API.Dtos
{
    public class UserDetailDto
    {
        public required string first_name { get; set; }

        public required string last_name { get; set; }

        public required decimal wallet_balance { get; set; }

        public required string referral_code { get; set; }

        public required int countUserReffer { get; set; }
        
        public required string time_stamp { get; set; }
        
        public VIPUserDetailDto? vip_detail { get; set; }
    }
}

