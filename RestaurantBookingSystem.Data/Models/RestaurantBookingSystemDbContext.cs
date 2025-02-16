using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Models
{
    public class RestaurantBookingSystemDbContext : DbContext
    {
        DbSet<Permission> Permissions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Restaurant> Restaurants { get; set; }
        DbSet<Menu> Menus { get; set; }
        DbSet<FoodItem> FoodItems { get; set; }
        DbSet<FoodCategory> FoodCategories { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<TableStatus> TableStatuses { get; set; }
        DbSet<Table> Tables { get; set; }
        DbSet<TableOrder> TablesOrders { get; set; }
        DbSet<ReservationStatus> ReservationStatuses { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<PaymentStatus> PaymentStatuses { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<OrderStatus> OrderStatuses { get; set; }
        DbSet<Customer> Customers { get; set; }
        public RestaurantBookingSystemDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
