using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration.RolesPermissions
{
    //[Authorize(Permissions = "Administrator")]
    public class EditPermissionModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public EditPermissionModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public Permission EditPermission { get; set; }

        public IActionResult OnGet(int id)
        {
            EditPermission = _userRepository.GetPermissionById(id);
            return Page();
        }

        public IActionResult OnPostEditPermission()
        {
            if (!string.IsNullOrWhiteSpace(EditPermission.Name))
            {
                _userRepository.UpdatePermission(EditPermission);
            }

            return RedirectToPage("/Administration/Index", new { view = "_RolesPermissions" });
        }
    }
}
