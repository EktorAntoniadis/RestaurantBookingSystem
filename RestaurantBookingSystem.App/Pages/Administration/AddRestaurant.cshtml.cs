using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AddRestaurantModel : PageModel
    {
        [BindProperty]
        public Restaurant NewRestaurant { get; set; }

        private readonly IRestaurantRepository _restaurantRepository;

        public AddRestaurantModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public IActionResult OnPost()
        {           
            _restaurantRepository.AddRestaurant(NewRestaurant);

            return RedirectToPage("/Administration/Index", new { view = "_Restaurants" } );
        }
    }
}
