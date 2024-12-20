using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _3laFeen.Domain.Entities
{
    public class User : IdentityUser
    {
        public long Roles { get; set; }
        [MaxLength(20)]
        public string Tenant { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public new string Email { get; set; }


    }
}
