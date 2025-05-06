using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.MyRestaurant
{
    public class EditMenuModel : PageModel
    {
        private readonly IRestaurantRepository _repository;

        public EditMenuModel(IRestaurantRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [BindProperty]
        public Restaurant MyRestaurant { get; set; }

        public IActionResult OnGet(int id)
        {
            MyRestaurant = _repository.GetRestaurantById(id);

            return Page();
        }

        public IActionResult OnPostUpdateMenu(int id, int menuId, string menuName, string menuDescription)
        {
            MyRestaurant = _repository.GetRestaurantById(id);
            if (MyRestaurant.Menu != null)
            {
                MyRestaurant.Menu.Name = menuName;
                MyRestaurant.Menu.Description = menuDescription;
            }
            _repository.UpdateRestaurant(MyRestaurant);
            return RedirectToPage();
        }

        public IActionResult OnPostAddCategory(int id, string categoryName, string categoryDescription)
        {
            MyRestaurant = _repository.GetRestaurantById(id);
            if (MyRestaurant.Menu != null)
            {
                MyRestaurant.Menu.FoodCategories.Add(new FoodCategory { Name = categoryName, Description = categoryDescription });
                _repository.UpdateRestaurant(MyRestaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveCategory(int id, int categoryId)
        {
            MyRestaurant = _repository.GetRestaurantById(id);
            var category = MyRestaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                MyRestaurant.Menu.FoodCategories.Remove(category);
                _repository.UpdateRestaurant(MyRestaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostAddFoodItem(int id, int categoryId, string foodItemName, string foodItemDescription)
        {
            MyRestaurant = _repository.GetRestaurantById(id);
            var category = MyRestaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                category.FoodItems.Add(new FoodItem { FoodCategory = category, Name = foodItemName, Description = foodItemDescription });
                _repository.UpdateRestaurant(MyRestaurant);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFoodItem(int id, int categoryId, int foodItemId)
        {
            MyRestaurant = _repository.GetRestaurantById(id);
            var category = MyRestaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            var foodItem = category.FoodItems.FirstOrDefault(x => x.Id == foodItemId);
            if (foodItem != null)
            {
                category.FoodItems.Remove(foodItem);
                _repository.UpdateRestaurant(MyRestaurant);
            }
            return RedirectToPage();
        }
    }
}
