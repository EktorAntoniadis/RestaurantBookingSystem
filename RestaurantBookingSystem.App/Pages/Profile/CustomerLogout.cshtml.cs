using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantBookingSystem.App.Pages.Profile
{
    public class CustomerLogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            Response.Cookies.Delete("jwt_token");
            return RedirectToPage("/Profile/CustomerLogin");
        }
    }
}
