using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantBookingSystem.Data.BaseModels;

namespace RestaurantBookingSystem.Data.Models
{
    public class FoodItem : CommonData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(FoodCategory))]
        public int FoodCategoryId { get; set; }
        public required FoodCategory FoodCategory { get; set; }
    }
}
