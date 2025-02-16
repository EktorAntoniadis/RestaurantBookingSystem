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
    public class TableOrder
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Table))]
        public int TableId { get; set; }

        public Table Table { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Reservation Reservation { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
