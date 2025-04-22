using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Employees
{
    public class ViewEmployeeModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ViewEmployeeModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public RestaurantUser ViewEmployee { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewEmployee = _userRepository.GetRestaurantUserById(id);

            if (ViewEmployee == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
