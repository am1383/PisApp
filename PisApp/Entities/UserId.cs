using System.ComponentModel.DataAnnotations;

namespace PisApp.API.Entities.Common
{
    public class UserId
    {
        [Key]
        public required int client_id { get; set; }
    }
}