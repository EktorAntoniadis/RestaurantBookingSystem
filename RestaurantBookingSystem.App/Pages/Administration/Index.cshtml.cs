using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AdminPageModel : PageModel
    {
        public string? ViewName { get; set; }
        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Dashboard" : view;
            return Page();
        }
    }
}
