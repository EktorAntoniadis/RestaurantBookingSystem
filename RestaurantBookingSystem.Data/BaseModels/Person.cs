using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Data.Models
{
    public abstract class Person
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public  string FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public  string LastName { get; set; }

        [EmailAddress]
        [Required]
        public  string Email { get; set; }

        [Required]
        public  string Phone { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Username should be 15 characters.")]
        public  string Username { get; set; }

        [Required]
        public  string Password { get; set; }
    }
}