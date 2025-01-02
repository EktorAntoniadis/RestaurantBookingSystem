using Microsoft.EntityFrameworkCore;

namespace RestaurantBookingSystem.Models
{
    public class RestaurantBookingSystemDbContext: DbContext
    {
        public RestaurantBookingSystemDbContext(DbContextOptions options) :  base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<FoodCategory> FoodCategorys { get; set; }
        public DbSet<FoodItem> FoodIthems { get; set; }
        public DbSet<TableOrder> TableOrders { get; set; }
        public DbSet<TableStatus> TableStatus { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ReservationStatus> ReservationStatus { get; set; }
    }
}
