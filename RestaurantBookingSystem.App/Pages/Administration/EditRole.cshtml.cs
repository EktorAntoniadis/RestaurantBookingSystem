using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    //[Authorize(Roles = "Administrator")]
    public class EditRoleModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public EditRoleModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public Role EditRole { get; set; }

        public IActionResult OnGet(int id)
        {
            EditRole = _userRepository.GetRoleById(id);
            return Page();
        }

        public IActionResult OnPostEditRole()
        {
            if (!string.IsNullOrWhiteSpace(EditRole.Name))
            {
                _userRepository.UpdateRole(EditRole);
            }

            return RedirectToPage("/Administration/Index");
        }
    }
}
