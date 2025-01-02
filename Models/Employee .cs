using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBookingSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime EndDate { get; set; }
        
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; } 
    }
}
