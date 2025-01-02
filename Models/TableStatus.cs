using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Models
{
    public class TableStatus
    {
        [Key]
        public int TableStatusId { get; set; }
        public string Status { get; set; }
    }
}
