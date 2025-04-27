using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Profile
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        
        public LoginModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            if(Request.Cookies.TryGetValue("jwt_token", out string token))
            {
                var (isValid, userType) = _userService.IsTokenValid(token);

                if (isValid)
                {
                    return userType switch
                    {
                        "SystemUser" => RedirectToPage("/Administration/Index", new { view = "_Dashboard" }),
                        "RestaurantUser" => RedirectToPage("/RestaurantUsers/Index", new { view = "_Dashboard" }),
                        _ => RedirectToPage("/Profile/Login")
                    };
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                var (token, userType, restaurantIdentifier) = _userService.CreateTokenForUer(Username, Password);
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(60)
                });

                return userType switch
                {
                    "SystemUser" => RedirectToPage("/Administration/Index", new { view = "_Dashboard" }),
                    "RestaurantUser" => RedirectToPage("/RestaurantUsers/Index", new { view = "_Dashboard" }),
                    _ => RedirectToPage("/Profile/Login")
                };

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
