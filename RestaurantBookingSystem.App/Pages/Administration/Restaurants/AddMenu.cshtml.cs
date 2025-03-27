using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Restaurants
{
    public class AddMenuModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public AddMenuModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public Restaurant Restaurant { get; set; }

        [BindProperty]
        public Menu NewMenu { get; set; }
        public IActionResult OnGet(int id)
        {
            Restaurant = _restaurantRepository.GetRestaurantById(id);
            NewMenu = new Menu()
            {
                Name = "",
                Description = "",
                FoodCategories = new List<FoodCategory>()
            };

            return Page();
        }

        public IActionResult OnPostAddMenu(int id)
        {
            Restaurant = _restaurantRepository.GetRestaurantById(id);

            if (Restaurant is null)
            {
                return NotFound();
            }

            Restaurant.Menu = NewMenu;
            _restaurantRepository.UpdateRestaurant(Restaurant);
            NewMenu = null;

            return Page();
        }

        public IActionResult OnPostAddCategory(int id, string categoryName, string categoryDescription)
        {
            Restaurant = _restaurantRepository.GetRestaurantById(id);
            Restaurant.Menu.FoodCategories.Add(new FoodCategory { Name = categoryName, Description = categoryDescription });
            _restaurantRepository.UpdateRestaurant(Restaurant);
            return RedirectToPage();
        }

        public IActionResult OnPostAddFoodItem(int id, int categoryId, string foodItemName, string foodItemDescription)
        {
            Restaurant = _restaurantRepository.GetRestaurantById(id);
            var category = Restaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                category.FoodItems.Add(new FoodItem { FoodCategory = category, Name = foodItemName, Description = foodItemDescription });
            }
            _restaurantRepository.UpdateRestaurant(Restaurant);
            return RedirectToPage();
        }        
    }
}
