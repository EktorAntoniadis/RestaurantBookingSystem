using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.SystemUsers
{
    public class DeleteSystemUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public DeleteSystemUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        [BindProperty]
        public SystemUser DeleteSystemUser { get; set; }

        public IActionResult OnGet(int id)
        {
            DeleteSystemUser = _userRepository.GetSystemUserById(id);
            if (DeleteSystemUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _userRepository.DeleteSystemUser(DeleteSystemUser.Id);
            return RedirectToPage("/Administration/Index", new { view = "_SystemUsers" });
        }
    }
}