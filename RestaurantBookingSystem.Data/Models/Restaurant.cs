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
    public class Restaurant : CommonData
    {
        [Key]
        public int Id { get; set; }

        public Menu Menu { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required string BusinessRegistrationNumber { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string PostalCode { get; set; }

        public ICollection<RestaurantUser> RestaurantUsers { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
