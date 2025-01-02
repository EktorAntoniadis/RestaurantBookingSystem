using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Models
{
    public class ReservationStatus
    {
        [Key]
        public int ReservationStatusId { get; set; }
        public string Status { get; set; }
    }
}
