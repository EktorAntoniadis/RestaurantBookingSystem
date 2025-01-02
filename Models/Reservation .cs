using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBookingSystem.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int NumberOfPeople { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }

        [ForeignKey("ReservationStatus")]
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
