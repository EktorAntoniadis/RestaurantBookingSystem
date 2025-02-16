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
    public class Payment: CommonData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Reservation))]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey(nameof(PaymentStatus))]
        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateOnly PaymentDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
