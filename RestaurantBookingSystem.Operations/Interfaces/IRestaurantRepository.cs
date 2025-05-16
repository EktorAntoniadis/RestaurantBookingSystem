using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;

namespace RestaurantBookingSystem.Operations.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        void AddRestaurant(Restaurant restaurant);
        Restaurant? GetRestaurantById(int id);
        PaginatedList<Restaurant> GetRestaurants(
            int pageIndex,
            int pageSize,
            string? name = null,
            string? menu = null,
            string? address = null,
            string? city = null,
            string? country = null,
            string? businessRegNo = null,
            string? postalCode = null,
            string? sortColumn = "Name",
            string? sortDirection = "asc");
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(int id);
        void AddMenu(Menu menu);
        Menu? GetMenuById(int id);
        IEnumerable<Menu> GetMenus();
        PaginatedList<Menu> GetMenu(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "name",
            string? sortDirection = "asc");
        void UpdateMenu(Menu menu);
        void DeleteMenu(int id);
        void AddFoodCategory(FoodCategory category);
        FoodCategory? GetFoodCategoryById(int id);
        IEnumerable<FoodCategory> GetFoodCategoriesByMenuId(int menuId);
        PaginatedList<FoodCategory> GetFoodCategory(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "name",
            string? sortDirection = "asc");
        void UpdateFoodCategory(FoodCategory category);
        void DeleteFoodCategory(int id);
        void AddFoodItem(FoodItem item);
        FoodItem? GetFoodItemById(int id);
        IEnumerable<FoodItem> GetFoodItemsByCategoryId(int categoryId);
        PaginatedList<FoodItem> GetFoodItems(
            int pageIndex,
            int pageSize,
            string? name,
            string? sortColumn = "name",
            string? sortDirection = "asc");
        void UpdateFoodItem(FoodItem item);
        void DeleteFoodItem(int id);

        List<Table> GetAllTables(int restaurantId);

        List<Table> GetTablesByStatus(int restaurantId, string status);

        List<TableStatus> GetTableStatuses();

        List<OrderStatus> GetOrderStatuses();
        List<ReservationStatus> GetReservationStatuses();

        void AddTable(Table table);

        void UpdateTable(Table table);

        Table? GetTableById(int id);

        public void AddReservation(Reservation reservation);

        public Reservation? GetReservationById(int id);

        public void UpdateReservation(Reservation reservation);

        public List<Table> GetAvailableTables(int restaurantId,
            DateOnly reservationDate,
            TimeOnly reservationTime,
            int numberOfPeople);

        public PaginatedList<Reservation> GetReservationsByRestaurant(
          int pageIndex,
          int pageSize,
          int restaurantId,
          DateOnly? reservationDate,
          string? sortColumn = "ReservationDate",
          string? sortDirection = "desc");


        public PaginatedList<TableOrder> GetOrders(
            int restaurantId,
            DateOnly? reservationDate,
            int pageIndex,
            int pageSize,
             string? sortColumn = "ReservationDate",
            string? sortDirection = "desc");

        public PaginatedList<Restaurant> GetRestaurantsByLocation(
            int pageIndex,
            int pageSize,
            string? restaurantName = null,
            string? address = null,
            string? city = null,
            string? country = null,
            string? sortColumn = "City",
            string? sortDirection = "desc");

        public PaginatedList<Reservation> GetCustomerReservations(
             int pageIndex,
            int pageSize,
            int customerId,
            string? sortColumn = "ReservationDate",
            string? sortDirection = "desc"
            );

    }
}
