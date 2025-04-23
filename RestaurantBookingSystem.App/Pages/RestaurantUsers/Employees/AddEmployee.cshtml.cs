using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Employees
{
    public class AddEmployeeModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private PasswordHasher<RestaurantUser> _passwordHasher;
        public AddEmployeeModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            AddEmployee = new RestaurantUser();
            _passwordHasher = new PasswordHasher<RestaurantUser>();
        }

        [BindProperty]
        public RestaurantUser AddEmployee { get; set; }

        public List<SelectListItem> RoleSelecList { get; set; }

        

        public IActionResult OnGet()
        {
            var roles = _userRepository.GetRoles();

            RoleSelecList = roles.Select(x =>
            new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();

            return Page();
        }

        public IActionResult OnPostAddNewEmployee()
        {

            ModelState.Remove("AddEmployee.Role");
            ModelState.Remove("AddEmployee.Reservations");

            if (!ModelState.IsValid)
            {
                var roles = _userRepository.GetRoles();

                RoleSelecList = roles.Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                }).ToList();

                return Page();
            }

            AddEmployee.JoinDate = DateTime.Now;
            AddEmployee.EndDate = null;
            AddEmployee.Password = _passwordHasher.HashPassword(AddEmployee, AddEmployee.Password);
            _userRepository.AddRestaurantUser(AddEmployee);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Employees" });
        }
    }
}