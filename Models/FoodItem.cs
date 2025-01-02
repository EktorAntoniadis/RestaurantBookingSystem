using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBookingSystem.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public string FoodItemDescription { get; set; }
       
        [ForeignKey("FoodCategory")]
        public int FoodCategoryId { get; set; }
        public FoodCategory FoodCategory { get; set; }
    }
}
