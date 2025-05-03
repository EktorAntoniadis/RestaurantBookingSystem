using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories.Implementations;
using System.Data;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers
{
    public class IndexModel : PageModel
    {
        public string? ViewName { get; set; }

        private readonly IUserRepository _userRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public IndexModel(
            IUserRepository userRepository,
            IRestaurantRepository restaurantRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public string? ViewRoles { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<RestaurantUser> Employees { get; set; }

        public IEnumerable<Table> AllTables { get; set; }

        public Restaurant MyRestaurant { get; set; }

        public string OwnerName { get; set; }

        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Dashboard" : view;

            var restaurantIdClaim = User.FindFirst("RestaurantId");
            var restaurantId = int.Parse(restaurantIdClaim.Value);
            var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
     

            if (view == "_Employees")
            {
                Employees = _userRepository.GetEmployeesByRestaurant(restaurantId);
            }

            if (view == "_Tables")
            {
                AllTables = _restaurantRepository.GetAllTables(restaurantId).OrderBy(x => x.Id);
            }

            if(view == "_MyRestaurant")
            {
                MyRestaurant = restaurant;
            }

            var businessOwner = restaurant.RestaurantUsers.FirstOrDefault(x => x.Role.Name == "Business Owner");
            OwnerName = $"{businessOwner.FirstName} {businessOwner.LastName}";

            return Page();
        }
    }
}
