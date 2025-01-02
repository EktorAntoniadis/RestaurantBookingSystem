using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBookingSystem.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public string TableName { get; set; }

        [ForeignKey("TableStatus")]
        public int TableStatusId { get; set; }
        public TableStatus TableStatus { get; set; }
    }
}
