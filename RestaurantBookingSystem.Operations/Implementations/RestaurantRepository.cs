using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantBookingSystem.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantBookingSystemDbContext _context;

        public RestaurantRepository(RestaurantBookingSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public Restaurant? GetRestaurantById(int id)
        {
            return _context.Restaurants.Find(id);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
        }

        public void DeleteRestaurant(int id)
        {
            var restaurant = GetRestaurantById(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
        }

        public void AddMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            _context.SaveChanges();
        }

        public Menu? GetMenuById(int id)
        {
            return _context.Menus
                .Include(x => x.FoodCategories)
                .ThenInclude(x => x.FoodItems)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Menu> GetMenus()
        {
            return _context.Menus.ToList();
        }

        public void UpdateMenu(Menu menu)
        {
            _context.Menus.Update(menu);
            _context.SaveChanges();
        }

        public void DeleteMenu(int id)
        {
            var menu = GetMenuById(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                _context.SaveChanges();
            }
        }
        public void AddFoodCategory(FoodCategory category)
        {
            _context.FoodCategories.Add(category);
            _context.SaveChanges();
        }

        public FoodCategory? GetFoodCategoryById(int id)
        {
            return _context.FoodCategories
                .Include(x => x.FoodItems)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<FoodCategory> GetFoodCategoriesByMenuId(int menuId)
        {
            return _context.FoodCategories
                .Include(x => x.FoodItems)
                .Where(fc => fc.MenuId == menuId).ToList();
        }

        public void UpdateFoodCategory(FoodCategory category)
        {
            _context.FoodCategories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteFoodCategory(int id)
        {
            var category = GetFoodCategoryById(id);
            if (category != null)
            {
                _context.FoodCategories.Remove(category);
                _context.SaveChanges();
            }
        }
        public void AddFoodItem(FoodItem item)
        {
            _context.FoodItems.Add(item);
            _context.SaveChanges();
        }

        public FoodItem? GetFoodItemById(int id)
        {
            return _context.FoodItems.Find(id);
        }

        public IEnumerable<FoodItem> GetFoodItemsByCategoryId(int categoryId)
        {
            return _context.FoodItems.Where(fi => fi.FoodCategoryId == categoryId).ToList();
        }

        public void UpdateFoodItem(FoodItem item)
        {
            _context.FoodItems.Update(item);
            _context.SaveChanges();
        }

        public void DeleteFoodItem(int id)
        {
            var item = GetFoodItemById(id);
            if (item != null)
            {
                _context.FoodItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public PaginatedList<Menu> GetMenu(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "Name",
            string? sortDirection = "asc")
        {
            var query = _context.Menus.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            switch (sortColumn)
            {
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = query.Count();
            var menus = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Menu>(menus, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<FoodCategory> GetFoodCategory(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "Name",
            string? sortDirection = "asc")
        {
            var query = _context.FoodCategories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            switch (sortColumn)
            {
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = query.Count();
            var categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<FoodCategory>(categories, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<FoodItem> GetFoodItems(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "Name",
            string? sortDirection = "asc")
        {
            var query = _context.FoodItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            switch (sortColumn)
            {
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = query.Count();
            var foodItems = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<FoodItem>(foodItems, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<Restaurant> GetRestaurants(int pageIndex,
            int pageSize,
            string? name = null,
            string? menu = null,
            string? address = null,
            string? city = null,
            string? country = null,
            string? businessRegNo = null,
            string? postalCode = null,
            string? sortColumn = "Name",
            string? sortDirection = "asc")
        {
            var query = _context.Restaurants.Include(x=>x.Menu).AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(menu))
            {
                query = query.Where(x => x.Menu.Name.Contains(menu));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(x => x.Address.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                query = query.Where(x => x.City == city);
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(x => x.Country == country);
            }

            if (!string.IsNullOrWhiteSpace(businessRegNo))
            {
                query = query.Where(x => x.BusinessRegistrationNumber == businessRegNo);
            }

            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                query = query.Where(x => x.PostalCode == postalCode);
            }

            switch (sortColumn)
            {
                case "Address":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Address) : query.OrderBy(x => x.Address);
                    break;
                case "City":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.City) : query.OrderBy(x => x.City);
                    break;
                case "Country":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Country) : query.OrderBy(x => x.Country);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                    break;
            }

            var totalRecords = query.Count();

            var restaurants = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Restaurant>(restaurants, totalRecords, pageIndex, pageSize);

        }
    }
}