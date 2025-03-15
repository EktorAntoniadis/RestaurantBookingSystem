using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AddRoleModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public AddRoleModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public Role NewRole { get; set; }

        [BindProperty]
        public List<int> SelectedPermissionIds { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }

        public IActionResult OnGet()
        {
            NewRole = new Role() { Name = "", Description = "" };
            Permissions = _userRepository.GetPermissions();
            return Page();
        }

        public IActionResult OnPostAddNewRole()
        {
            Permissions = _userRepository.GetPermissions();
            NewRole.Permissions = Permissions.Where(x => SelectedPermissionIds.Contains(x.Id)).ToList();
            _userRepository.AddRole(NewRole);
            return RedirectToPage("/Administration/Index", new { view = "_RolesPermissions" });
        }
    }
}