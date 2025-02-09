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
    public class Menu: CommonData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }
        public Restaurant Tenant { get; set; }
        public ICollection<FoodCategory> FoodCategories { get; set; }
    }
}
