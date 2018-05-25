using System.ComponentModel.DataAnnotations;

namespace BeerOn.Data.ModelsDto.User
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
