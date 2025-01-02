using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
