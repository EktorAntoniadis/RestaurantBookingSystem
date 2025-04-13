using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.SystemUsers
{
    public class ViewSystemUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public ViewSystemUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public SystemUser ViewSystemUser { get; set; }

        public IActionResult OnGet(int id)
        {
            ViewSystemUser = _userRepository.GetSystemUserById(id);

            if (ViewSystemUser == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}