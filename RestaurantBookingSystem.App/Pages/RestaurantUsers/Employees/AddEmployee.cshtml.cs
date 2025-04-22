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
        [BindProperty]
        public RestaurantUser AddEmployee { get; set; }

        public List<SelectListItem> RoleSelecList { get; set; }

        private readonly IUserRepository _userRepository;

        public AddEmployeeModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            AddEmployee = new RestaurantUser();
        }

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
            _userRepository.AddRestaurantUser(AddEmployee);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Employees" });
        }
    }
}