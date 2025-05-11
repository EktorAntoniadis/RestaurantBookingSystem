using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

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
            return _context.Restaurants
                .Include(x => x.Menu)
                .ThenInclude(x => x.FoodCategories)
                .ThenInclude(x => x.FoodItems)
                .Include(x => x.RestaurantUsers)
                .ThenInclude(x => x.Role)
                .FirstOrDefault(x => x.Id == id);
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
            var query = _context.Restaurants.Include(x => x.Menu).AsQueryable();

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

        public List<Table> GetAllTables(int restaurantId)
        {
            return _context.Tables
                           .Include(x => x.Status)
                           .Where(t => t.RestaurantId == restaurantId)
                           .ToList();
        }

        public List<Table> GetTablesByStatus(int restaurantId, string status)
        {
            return _context.Tables
                           .Where(t => t.RestaurantId == restaurantId
                           && t.Status.Status == status)
                           .ToList();
        }

        public List<TableStatus> GetTableStatuses()
        {
            return _context.TableStatuses.ToList();
        }

        public List<OrderStatus> GetOrderStatuses()
        {
            return _context.OrderStatuses.ToList();
        }

        public List<ReservationStatus> GetReservationStatuses()
        {
            return _context.ReservationStatuses.ToList();
        }

        public void AddTable(Table table)
        {
            _context.Tables.Add(table);
            _context.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            _context.Tables.Update(table);
            _context.SaveChanges();
        }

        public Table? GetTableById(int id)
        {
            return _context.Tables
                .Include(x => x.Restaurant)
                .Include(x => x.Status)
                .FirstOrDefault(t => t.Id == id);
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public Reservation? GetReservationById(int id)
        {
            return _context.Reservations
                .Include(x => x.Customer)
                .Include(x => x.TableOrder)
                .ThenInclude(x => x.OrderStatus)
                .Include(x => x.ReservationStatus)
                .FirstOrDefault(x => x.Id == id);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public List<Table> GetAvailableTables(int restaurantId, DateOnly reservationDate, TimeOnly reservationTime, int numberOfPeople)
        {
            var reservedTableIds = _context.Reservations
                .Where(x => x.RestaurantId == restaurantId &&
                x.ReservationDate == reservationDate &&
                x.ReservationTime == reservationTime)
                .Select(r => r.TableOrder.TableId)
                .Distinct().ToList();

            var availalableTables = _context.Tables.Where(t => t.RestaurantId == restaurantId &&
            t.NumberOfPeople == numberOfPeople &&
            !reservedTableIds.Contains(t.Id) &&
            t.Status.Status != "Out of Service").ToList();

            return availalableTables;
        }

        public PaginatedList<Reservation> GetReservationsByRestaurant(
            int pageIndex,
            int pageSize,
            int restaurantId,
            DateOnly? reservationDate,
            string? sortColumn = "ReservationDate",
            string? sortDirection = "desc")
        {
            var query = _context.Reservations
                .Include(x => x.Customer)
                .Include(x => x.ReservationStatus)
                .Include(x => x.TableOrder)
                    .ThenInclude(x => x.Table)
                .Where(x => x.RestaurantId == restaurantId)
                .AsQueryable();

            if (reservationDate.HasValue)
            {
                query = query.Where(x => x.ReservationDate == reservationDate.Value);
            }

            switch (sortColumn)
            {
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.ReservationDate) : query.OrderBy(x => x.ReservationDate);
                    break;
            }

            var totalRecords = query.Count();

            var reservations = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<Reservation>(reservations, totalRecords, pageIndex, pageSize);
        }
    }
}