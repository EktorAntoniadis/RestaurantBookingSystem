using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Repositories.Implementations;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;
using System.Data;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers
{
    public class IndexModel : PageModel
    {
        public string? ViewName { get; set; }

        private readonly IUserRepository _userRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantService _restaurantService;

        public IndexModel(
            IUserRepository userRepository,
            IRestaurantRepository restaurantRepository,
            IRestaurantService restaurantService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; } = "desc";

        [FromQuery]
        public DateOnly ReservationDate { get; set; }

        [FromQuery]
        public string CustomerFirstName { get; set; }

        [FromQuery]
        public string CustomerLastName { get; set; }
        [FromQuery]
        public string CustomerPhoneNumber { get; set; }

        public IEnumerable<RestaurantUser> Employees { get; set; }

        public IEnumerable<Table> AllTables { get; set; }

        public Restaurant MyRestaurant { get; set; }
        public PaginatedList<Customer> MyCustomers { get; set; }

        public PaginatedList<TableOrder> MyOrders { get; set; }

        public string OwnerName { get; set; }

        public PaginatedList<Reservation> RestaurantReservations { get; set; }

        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Dashboard" : view;

            var restaurantIdClaim = User.FindFirst("RestaurantId");
            var restaurantId = int.Parse(restaurantIdClaim.Value);
            var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
     

            if (view == "_Employees")
            {
                Employees = _userRepository.GetEmployeesByRestaurant(restaurantId);
            }

            if (view == "_Tables")
            {
                AllTables = _restaurantRepository.GetAllTables(restaurantId).OrderBy(x => x.Id);
            }

            if(view == "_MyRestaurant")
            {
                MyRestaurant = restaurant;
            }


            if (view == "_Reservations")
            {
                SortColumn = "ReservationDate";
                RestaurantReservations = _restaurantRepository.GetReservationsByRestaurant(
                    PageIndex,
                    10,
                    restaurantId,
                    DateOnly.FromDateTime(DateTime.Today),
                    SortColumn,
                    SortDirection);
            }

            if (view == "_Customers")
            {
                SortColumn = "LastName";
                MyCustomers = _userRepository.GetCustomersByRestaurant(
                    restaurantId,
                    PageIndex,
                    10,
                    CustomerFirstName,
                    CustomerLastName,
                    CustomerPhoneNumber,
                    SortColumn,
                    SortDirection);
            }

            if (view == "_Orders")
            {
                SortColumn = "ReservationDate";
                MyOrders = _restaurantRepository.GetOrders(
                    restaurantId,
                    ReservationDate,
                    PageIndex,
                    10,
                    SortColumn,
                    SortDirection);
            }

            var businessOwner = restaurant.RestaurantUsers.FirstOrDefault(x => x.Role.Name == "Business Owner");
            OwnerName = $"{businessOwner.FirstName} {businessOwner.LastName}";

            return Page();
        }

        public IActionResult OnGetDailyReservations(DateOnly date)
        {
            ViewName = "_Reservations";
            var restaurantIdClaim = User.FindFirst("RestaurantId");
            var restaurantId = int.Parse(restaurantIdClaim.Value);
            RestaurantReservations = _restaurantRepository.GetReservationsByRestaurant(
                   PageIndex,
                   10,
                   restaurantId,
                   date,
                   SortColumn,
                   SortDirection);
            return Page();
        }

        public IActionResult OnPostCancel(int id)
        {
            ViewName = "_Reservations";
            _restaurantService.CancelReservation(id);
            return Page();
        }

        public IActionResult OnPostConfirm(int id)
        {
            ViewName = "_Reservations";
            _restaurantService.ConfirmReservation(id);
            return Page();
        }
    }
}


