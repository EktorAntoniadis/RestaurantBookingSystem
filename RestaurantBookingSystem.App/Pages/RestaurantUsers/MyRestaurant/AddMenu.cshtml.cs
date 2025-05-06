using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.MyRestaurant
{
    public class AddMenuModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public AddMenuModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public Restaurant MyRestaurant { get; set; }

        [BindProperty]
        public Menu NewMenu { get; set; }
        public IActionResult OnGet(int id)
        {
            MyRestaurant = _restaurantRepository.GetRestaurantById(id);
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
            MyRestaurant = _restaurantRepository.GetRestaurantById(id);

            if (MyRestaurant is null)
            {
                return NotFound();
            }

            MyRestaurant.Menu = NewMenu;
            _restaurantRepository.UpdateRestaurant(MyRestaurant);
            NewMenu = null;

            return Page();
        }

        public IActionResult OnPostAddCategory(int id, string categoryName, string categoryDescription)
        {
            MyRestaurant = _restaurantRepository.GetRestaurantById(id);
            MyRestaurant.Menu.FoodCategories.Add(new FoodCategory { Name = categoryName, Description = categoryDescription });
            _restaurantRepository.UpdateRestaurant(MyRestaurant);
            return RedirectToPage();
        }

        public IActionResult OnPostAddFoodItem(int id, int categoryId, string foodItemName, string foodItemDescription)
        {
            MyRestaurant = _restaurantRepository.GetRestaurantById(id);
            var category = MyRestaurant.Menu.FoodCategories.FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                category.FoodItems.Add(new FoodItem { FoodCategory = category, Name = foodItemName, Description = foodItemDescription });
            }
            _restaurantRepository.UpdateRestaurant(MyRestaurant);
            return RedirectToPage();
        }
    }
}
