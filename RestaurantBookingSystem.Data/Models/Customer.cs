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
    public class Customer: Person
    {
        [Key]
        public int Id { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public TimeOnly RegistrationTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
