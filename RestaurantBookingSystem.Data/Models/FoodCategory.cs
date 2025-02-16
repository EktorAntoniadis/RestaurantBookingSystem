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
    public class FoodCategory: CommonData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Menu))]
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
