namespace PisApp.API.Entities
{
    public class UserDetail 
    {
        public required string first_name { get; set; }

        public required string last_name { get; set; }

        public required decimal wallet_balance { get; set; }

        public required string referral_code { get; set; }
        
        public required DateTime time_stamp { get; set; }

        public string FormattedTimeStamp => time_stamp.ToString("yyyy-MM-dd HH:mm:ss");
    }
}