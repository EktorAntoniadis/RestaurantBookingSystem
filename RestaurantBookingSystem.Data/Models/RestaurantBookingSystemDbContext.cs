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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
        public DbSet<TableStatus> TableStatuses { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableOrder> TablesOrders { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<SystemUser> SystemUsers { get; set; }

        public RestaurantBookingSystemDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
