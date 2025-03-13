using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class DeleteRoleModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeleteRoleModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public Role DeleteRole { get; set; }

        public IActionResult OnGet(int id)
        {
            DeleteRole = _userRepository.GetRoleById(id);
            if (DeleteRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _userRepository.DeleteRole(DeleteRole.Id);
            return RedirectToPage("/Administration/Index");
        }
    }
}