using RestaurantBookingSystem.Data.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class RestaurantUser : Person
    {
        [Key]
        public int Id { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
