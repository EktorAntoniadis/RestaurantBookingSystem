using RestaurantBookingSystem.Data.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class Role : CommonData
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
