using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public int RoleName { get; set; }
    }
}
