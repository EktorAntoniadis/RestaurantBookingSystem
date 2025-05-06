using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.MyRestaurant
{
    public class DeleteMenuModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        [BindProperty]
        public Restaurant MyRestaurant { get; set; }

        public DeleteMenuModel(IRestaurantRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IActionResult OnGet(int id)
        {
            MyRestaurant = _repository.GetRestaurantById(id);

            if (MyRestaurant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int menuId)
        {
            _repository.DeleteMenu(menuId);
            return RedirectToPage("/RestaurantUsers/Index", new { view = "_MyRestaurant" });
        }
    }
}