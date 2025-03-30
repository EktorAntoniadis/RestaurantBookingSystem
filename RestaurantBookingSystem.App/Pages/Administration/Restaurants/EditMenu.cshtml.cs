using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories.Implementations;

namespace RestaurantBookingSystem.App.Pages.Administration.Restaurants
{
    public class EditMenuModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        public EditMenuModel(IRestaurantRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(int id)
        {
            Restaurant = _repository.GetRestaurantById(id);

            return Page();
        }

        public IActionResult OnPostUpdateMenu(int id, int menuId, string menuName, string menuDescription)
        {
            Restaurant = _repository.GetRestaurantById(id);
            if (Restaurant.Menu != null)
            {
                Restaurant.Menu.Name = menuName;
                Restaurant.Menu.Description = menuDescription;
            }
            _repository.UpdateRestaurant(Restaurant);
            return RedirectToPage();
        }

        public IActionResult OnPostAddCategory(int id, string categoryName, string categoryDescription)
        {
            Restaurant = _repository.GetRestaurantById(id);
            if (Restaurant.Menu != null)
            {
                Restaurant.Menu.FoodCategories.Add(new FoodCategory { Name = categoryName, Description = categoryDescription });
                _repository.UpdateRestaurant(Restaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveCategory(int id, int categoryId)
        {
            Restaurant = _repository.GetRestaurantById(id);
            var category = Restaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                Restaurant.Menu.FoodCategories.Remove(category);
                _repository.UpdateRestaurant(Restaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostAddFoodItem(int id, int categoryId, string foodItemName, string foodItemDescription)
        {
            Restaurant = _repository.GetRestaurantById(id);
            var category = Restaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                category.FoodItems.Add(new FoodItem { FoodCategory = category, Name = foodItemName, Description = foodItemDescription });
                _repository.UpdateRestaurant(Restaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFoodItem(int id, int categoryId, int foodItemId)
        {
            Restaurant = _repository.GetRestaurantById(id);
            var category = Restaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            var foodItem = category.FoodItems.FirstOrDefault(x => x.Id == foodItemId);
            if (foodItem != null) 
            {
                category.FoodItems.Remove(foodItem);
                _repository.UpdateRestaurant(Restaurant);
            }
            return RedirectToPage();
        }
    }
}
