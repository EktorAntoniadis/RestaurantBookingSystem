using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Models
{
    public class FoodCategory
    {
        [Key]
        public int FoodCategoryId { get; set; }
        public string FoodCategoryName { get; set; }
    }
}
