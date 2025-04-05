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
            Restaurants = _restaurantRepository.GetRestaurants(1, 10, Name);
            RestaurantId = EditRestaurantOwner.RestaurantId;
            return Page();
        }

        public IActionResult OnPostEditCurrentClient()
        {
            _userRepository.UpdateRestaurantUser(EditRestaurantOwner);
            return RedirectToPage("/Administration/Index", new { view = "_Clients" });
        }

        public IActionResult OnPostAssociateRestaurant(int id)
        {
            if (RestaurantId is null || RestaurantId == 0)
            {
                ModelState.AddModelError("RestaurantId", "Please select a restaurant.");
                Restaurants = _restaurantRepository.GetRestaurants(1, 10, Name);
                return Page();
            }
            EditRestaurantOwner = _userRepository.GetRestaurantUserById(id);

            var existingRestaurant = _restaurantRepository.GetRestaurantById(RestaurantId.Value);

            var existingRestaurantOwner = existingRestaurant?.RestaurantUsers.FirstOrDefault(x => x.Role.Name == "Business Owner");

            if(existingRestaurantOwner != null)
            {
                ModelState.AddModelError("RestaurantId", "This restaurant is already associated with another owner");
                EditRestaurantOwner = _userRepository.GetRestaurantUserById(id);
                Restaurants = _restaurantRepository.GetRestaurants(1, 10, Name);
                return Page();
            }

            EditRestaurantOwner.RestaurantId = RestaurantId;

            _userRepository.UpdateRestaurantUser(EditRestaurantOwner);

            return RedirectToPage("/Administration/Index", new { view = "_Clients" });
        }
    }
}
