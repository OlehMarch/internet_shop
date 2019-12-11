using System.ComponentModel.DataAnnotations;

namespace internet_shop.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
