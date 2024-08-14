using System.ComponentModel.DataAnnotations;

namespace ProductInventoryApp.DTO
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
