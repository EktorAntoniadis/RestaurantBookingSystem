using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.SystemUsers
{
    public class EditSystemUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public EditSystemUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [BindProperty]
        public SystemUser EditSystemUser { get; set; }

        public List<SelectListItem> RolesSelectList { get; set; }

        public IActionResult OnGet(int id)
        {
            EditSystemUser = _userRepository.GetSystemUserById(id);
            var roles = _userRepository.GetRoles();

            RolesSelectList = roles.Select(role => new SelectListItem
            {
                Value = role.Id.ToString(),
                Text = role.Name,

            }).ToList();
            return Page();
        }

        public IActionResult OnPostUpdateSystemUser()
        {
            ModelState.Remove("EditSystemUser.Role");

            if (!ModelState.IsValid)
            {
                var roles = _userRepository.GetRoles();

                RolesSelectList = roles.Select(role => new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name,

                }).ToList();
                return Page();
            }
            
            _userRepository.UpdateSystemUser(EditSystemUser);
            
            return RedirectToPage("/Administration/Index", new { view = "_SystemUsers" });
        }
    }
}
