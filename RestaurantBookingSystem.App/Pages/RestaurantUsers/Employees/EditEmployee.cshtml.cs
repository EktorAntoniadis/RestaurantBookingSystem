using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Employees
{
    public class EditEmployeeModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public EditEmployeeModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]

        public RestaurantUser EditEmployee { get; set; }
        public IActionResult OnGet(int id)
        {
            EditEmployee = _userRepository.GetRestaurantUserById(id);
            return Page();
        }

        public IActionResult OnPostEditExistingEmployee()
        {
            _userRepository.UpdateRestaurantUser(EditEmployee);
            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Employees" });
        }        
    }
}
