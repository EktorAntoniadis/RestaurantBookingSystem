using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.Data.Models;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AddRestaurantModel : PageModel
    {
        [BindProperty]
        public Restaurant NewRestaurant { get; set; }

        private readonly RestaurantBookingSystemDbContext _context;

        public AddRestaurantModel(RestaurantBookingSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult OnPost()
        {
            _context.Restaurants.Add(NewRestaurant);
            _context.SaveChanges();

            return RedirectToPage("/Administration/AdminPage");
        }
    }
}
