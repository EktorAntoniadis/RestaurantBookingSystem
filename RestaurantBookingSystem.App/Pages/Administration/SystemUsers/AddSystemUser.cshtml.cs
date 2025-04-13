using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories.Implementations;

namespace RestaurantBookingSystem.App.Pages.Administration.SystemUsers
{
    public class AddSystemUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private PasswordHasher<SystemUser> _passwordHasher;

        public AddSystemUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = new PasswordHasher<SystemUser>();
        }

        [BindProperty]
        public SystemUser NewSystemUser { get; set; }

        public List<SelectListItem> RolesSelectList { get; set; }

        public IActionResult OnGet()
        {
            NewSystemUser = new SystemUser() { FirstName = "", LastName = "" };
            var roles = _userRepository.GetRoles();

            RolesSelectList = roles.Select(role => new SelectListItem
            {
                Value = role.Id.ToString(),
                Text = role.Name,

            }).ToList();
            return Page();
        }

        public IActionResult OnPostAddSystemUser()
        {
            ModelState.Remove("NewSystemUser.Role");
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

            NewSystemUser.JoinDate = DateTime.Now;
            NewSystemUser.EndDate = null;
            NewSystemUser.Password = _passwordHasher.HashPassword(NewSystemUser, NewSystemUser.Password!);
            _userRepository.AddSystemUser(NewSystemUser);
            return RedirectToPage("/Administration/Index", new { view = "_SystemUsers" });
        }
    }
}