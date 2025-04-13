using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class SystemUser : Person
    {
        [Key]
        public int Id { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(Role))]
        [Required(ErrorMessage = "Role is required")]
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
