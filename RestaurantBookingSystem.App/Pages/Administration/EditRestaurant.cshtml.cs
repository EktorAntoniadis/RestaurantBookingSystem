using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;
using RestaurantBookingSystem.Repositories.Implementations;

namespace RestaurantBookingSystem.Pages.Restaurants
{
    public class EditRestaurantModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        [BindProperty]
        public Restaurant EditRestaurant { get; set; }

        public EditRestaurantModel(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            EditRestaurant = _repository.GetRestaurantById(id);
            if (EditRestaurant == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
           
            _repository.UpdateRestaurant(EditRestaurant);
            return RedirectToPage("/Administration/Index", new { view = "_Restaurants" });
        }
    }
}
