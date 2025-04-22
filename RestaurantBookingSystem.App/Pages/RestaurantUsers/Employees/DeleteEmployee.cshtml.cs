using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Employees
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public RestaurantUser EmployeeToDelete { get; set; }

        public DeleteEmployeeModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        }

        public IActionResult OnGet(int id)
        {
            EmployeeToDelete = _userRepository.GetRestaurantUserById(id);
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            _userRepository.DeleteRestaurantUser(id);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Employees" });
        }
    }
}
