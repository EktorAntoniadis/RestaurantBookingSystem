using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Profile
{
    public class CustomerLoginModel : PageModel
    {
        private IUserService _userService;

        public CustomerLoginModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnGet()
        {
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

        public IActionResult OnPost()
        {
            try
            {
                var token = _userService.CreateTokenForCustomer(Username, Password);
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(60)
                });

                return RedirectToPage("/Customers/Index", new { view = "_Reservations" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
