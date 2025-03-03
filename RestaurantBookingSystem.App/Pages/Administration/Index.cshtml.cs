using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AdminPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public AdminPageModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public string? ViewName { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Dashboard" : view;

            if(view == "_RolesPermissions")
            {
                Roles = _userRepository.GetRoles();
            }

            return Page();
        }
    }
}
