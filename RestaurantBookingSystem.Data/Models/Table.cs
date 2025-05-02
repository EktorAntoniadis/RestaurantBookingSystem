using RestaurantBookingSystem.Data.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }

        public int NumberOfPeople { get; set; }

        [ForeignKey(nameof(TableStatus))]
        public int TableStatusId { get; set; }
        public required TableStatus Status { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
