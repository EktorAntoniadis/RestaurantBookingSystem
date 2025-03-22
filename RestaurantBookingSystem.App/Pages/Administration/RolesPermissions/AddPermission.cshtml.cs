using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration.RolesPermissions
{
    public class AddPermissionModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public AddPermissionModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public Permission NewPermission { get; set; }

        public IActionResult OnGet()
        {
            NewPermission = new Permission() { Name = "", Description = "" };
            return Page();
        }

        public IActionResult OnPostAddNewPermission()
        {

            _userRepository.AddPermission(NewPermission);
            return RedirectToPage("/Administration/Index", new { view = "_RolesPermissions" });
        }
    }
}