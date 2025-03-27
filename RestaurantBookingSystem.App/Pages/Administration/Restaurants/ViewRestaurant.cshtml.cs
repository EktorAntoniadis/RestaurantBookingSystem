using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Restaurants
{
    public class ViewRestaurantModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public ViewRestaurantModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        [BindProperty]
        public Restaurant ViewRestaurant { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewRestaurant = _restaurantRepository.GetRestaurantById(id);
            return Page();
        }       
    }
}