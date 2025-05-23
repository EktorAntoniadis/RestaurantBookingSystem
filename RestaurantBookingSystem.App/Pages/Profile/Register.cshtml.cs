using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Profile
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<Customer> _hasher;
        private readonly IUserService _userService;

        public RegisterModel(
            IUserRepository userRepository,
            IUserService userService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _hasher = new PasswordHasher<Customer>();
        }

        [BindProperty]
        public Customer RegisterCustomer { get; set; }

        public string ErrorMessage { get; set; }
        public IActionResult OnGet()
        {
            RegisterCustomer = new Customer();
            if (Request.Cookies.TryGetValue("jwt_token", out string token))
            {
                var (isValid, userType) = _userService.IsTokenValid(token);

                if (isValid)
                {
                    return RedirectToPage("/Customers/Index", new { view = "_Reservations" });

                }
            }
            return Page();
        }

        public IActionResult OnPostRegisterMember()
        {
            ModelState.Remove("RegisterCustomer.Reservations");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userRepository.IsExistingCustomer(RegisterCustomer))
            {
                ErrorMessage = "You already have an account.";
                return Page();
            }

            RegisterCustomer.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
            RegisterCustomer.RegistrationTime = TimeOnly.FromDateTime(DateTime.Now);
            RegisterCustomer.Password = _hasher.HashPassword(RegisterCustomer, RegisterCustomer.Password);

            _userRepository.AddCustomer(RegisterCustomer);
            return RedirectToPage("/Profile/CustomerLogin");
        }
    }
}
