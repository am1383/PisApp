using System.ComponentModel.DataAnnotations;

namespace PisApp.API.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public string? phone_number { get; set; }
    }
}