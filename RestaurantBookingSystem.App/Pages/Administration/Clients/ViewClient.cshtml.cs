using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Clients
{
    public class ViewClientModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ViewClientModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public RestaurantUser ViewRestaurantOwner { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewRestaurantOwner = _userRepository.GetRestaurantUserById(id);

            if (ViewRestaurantOwner == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
