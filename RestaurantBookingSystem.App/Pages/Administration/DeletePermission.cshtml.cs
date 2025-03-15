using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;
using System.Data;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class DeletePermissionModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeletePermissionModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        [BindProperty]
        public Permission DeletePermission { get; set; }

        public IActionResult OnGet(int id)
        {
            DeletePermission = _userRepository.GetPermissionById(id);
            if (DeletePermission == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _userRepository.DeletePermission(DeletePermission.Id);
            return RedirectToPage("/Administration/Index", new { view = "_RolesPermissions" });
        }
    }
}