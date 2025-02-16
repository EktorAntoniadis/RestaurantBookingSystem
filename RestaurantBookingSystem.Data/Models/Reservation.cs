using RestaurantBookingSystem.Data.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }

        [ForeignKey(nameof(ReservationStatus))]
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        [ForeignKey(nameof(TableOrder))]
        public int TableOrderId { get; set; }
        public TableOrder TableOrder { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
