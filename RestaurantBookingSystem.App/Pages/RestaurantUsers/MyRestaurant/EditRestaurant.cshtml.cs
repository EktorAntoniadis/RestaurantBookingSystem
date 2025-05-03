using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.MyRestaurant
{
    public class EditRestaurantModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;

        [BindProperty]
        public Restaurant EditRestaurant { get; set; }

        public EditRestaurantModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public IActionResult OnGet(int id)
        {
            EditRestaurant = _restaurantRepository.GetRestaurantById(id);
            if (EditRestaurant == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostEditMyRestaurant()
        {

            _restaurantRepository.UpdateRestaurant(EditRestaurant);
            return RedirectToPage("/RestaurantUsers/Index", new { view = "_MyRestaurant" });
        }
    }
}
