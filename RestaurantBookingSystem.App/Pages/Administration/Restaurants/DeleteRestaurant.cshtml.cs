using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories;
using RestaurantBookingSystem.Repositories.Implementations;

namespace RestaurantBookingSystem.App.Pages.Administration.Restaurants
{
    public class DeleteRestaurantModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        [BindProperty]
        public Restaurant DeleteRestaurant { get; set; }

        public DeleteRestaurantModel(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet(int id)
        {
            DeleteRestaurant = _repository.GetRestaurantById(id);

            if (DeleteRestaurant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            DeleteRestaurant = _repository.GetRestaurantById(id);

            if (DeleteRestaurant == null)
            {
                return NotFound();
            }

            _repository.DeleteRestaurant(DeleteRestaurant.Id);
            return RedirectToPage("/Administration/Index", new { view = "_Restaurants" });
        }
    }
}
