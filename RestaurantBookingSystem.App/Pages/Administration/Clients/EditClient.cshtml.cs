using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Clients
{
    public class EditClientModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        private readonly IRestaurantRepository _restaurantRepository;
        public EditClientModel(IUserRepository userRepository, IRestaurantRepository restaurantRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        [BindProperty]

        public RestaurantUser EditRestaurantOwner { get; set; }

        [BindProperty]
        public int? RestaurantId { get; set; }

        [FromQuery]
        public string Name { get; set; }

        public PaginatedList<Restaurant> Restaurants { get; set; }

        public IActionResult OnGet(int id)
        {
            EditRestaurantOwner = _userRepository.GetRestaurantUserById(id);
            RestaurantId = EditRestaurantOwner.RestaurantId;
            return Page();
        }

        public IActionResult OnPostEditCurrentClient()
        {
            _userRepository.UpdateRestaurantUser(EditRestaurantOwner);
            return RedirectToPage("/Administration/Index", new { view = "_Clients" });
        }
    }
}
