using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using System.Security.Claims;

namespace RestaurantBookingSystem.App.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public IndexModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public string? ViewName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Address { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Country { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortDirection { get; set; }

        [BindProperty]
        public Reservation NewReservation { get; set; }

        public PaginatedList<Reservation> CustomerReservations { get; set; }

        public PaginatedList<Restaurant> Restaurants { get; set; }

        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Reservations" : view;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var userCity = User.FindFirst(ClaimTypes.Locality)?.Value;
            var userCountry = User.FindFirst(ClaimTypes.Country)?.Value;
            var userIdValue = int.Parse(userId?.Value ?? "0");

            if (view == "_Reservations")
            {
                CustomerReservations = _restaurantRepository.GetPastReservationsByUser(PageIndex, 10, userIdValue);
            }

            if (view == "_BookAReservation")
            {
                City = string.IsNullOrEmpty(City) ? userCity : City;
                Country = string.IsNullOrEmpty(Country) ? userCountry : Country;
                Restaurants = _restaurantRepository.GetRestaurantsByLocation(PageIndex, 10, Name, Address, City, Country, "City", "desc");
            }

            return Page();
        }

        public IActionResult OnPostFindRestaurant(string view)
        {
            if (view == "_BookAReservation")
            {
                ViewName = view;
                Restaurants = _restaurantRepository.GetRestaurantsByLocation(PageIndex, 10, Name, Address, City, Country, "City", "desc");
            }
            return Page();
        }
    }
}
