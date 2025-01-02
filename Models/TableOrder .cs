using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBookingSystem.Models
{
    public class TableOrder
    {
        [Key]
        public int TableOrderId { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }

        [ForeignKey("FoodItem")]
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
