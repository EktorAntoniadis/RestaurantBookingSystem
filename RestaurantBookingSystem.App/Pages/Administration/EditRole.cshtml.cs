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

        [BindProperty]
        public List<int> SelectedPermissionIds { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }

        public IActionResult OnGet(int id)
        {
            EditRole = _userRepository.GetRoleById(id);
            Permissions = _userRepository.GetPermissions();
            SelectedPermissionIds = EditRole.Permissions.Select(x => x.Id).ToList();
            return Page();
        }

        public IActionResult OnPostEditRole()
        {
            _userRepository.UpdateRole(EditRole, SelectedPermissionIds);

            return RedirectToPage("/Administration/Index", new { view ="_RolesPermissions" });
        }
    }
}
