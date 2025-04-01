using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Restaurants
{
    public class DeleteMenuModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public DeleteMenuModel(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant = _repository.GetRestaurantById(id);

            if (Restaurant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int menuId)
        {
           _repository.DeleteMenu(menuId);
            return RedirectToPage("/Administration/Index", new { view = "_Restaurants" });
        }
    }
}