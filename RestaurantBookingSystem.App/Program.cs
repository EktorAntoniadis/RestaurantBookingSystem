using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Data.Seeding;
using RestaurantBookingSystem.Repositories;
using RestaurantBookingSystem.Repositories.Implementations;

namespace RestaurantBookingSystem.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<RestaurantBookingSystemDbContext>(options =>
                options.UseMySql("server=127.0.0.1;uid=root;database=restaurantbookingdb",
                ServerVersion.AutoDetect("server=127.0.0.1;uid=root;database=restaurantbookingdb")));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<RestaurantBookingSystemDbContext>();

                var seeder = new DatabaseSeed(context);
                seeder.SeedAdministrationData();
            }

            app.Run();
        }
    }
}
