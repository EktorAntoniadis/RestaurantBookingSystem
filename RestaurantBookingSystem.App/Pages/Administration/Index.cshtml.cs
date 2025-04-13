using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration
{
    public class AdminPageModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        public AdminPageModel(
            IUserRepository userRepository,
            IRestaurantRepository restaurantRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        public string? ViewName { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }

        public PaginatedList<Restaurant> Restaurants { get; set; }

        public PaginatedList<RestaurantUser> RestaurantOwners { get; set; }

        public PaginatedList<SystemUser> SystemUsers { get; set; }

        [FromQuery]
        public string? FirstName { get; set; }


        [FromQuery]
        public string? LastName { get; set; }

        [FromQuery]
        public string? Username { get; set; }

        [FromQuery]
        public DateTime? JoinDate { get; set; }

        [FromQuery]
        public DateTime? EndDate { get; set; }

        [FromQuery]
        public string? Name { get; set; }

        [FromQuery]
        public string? Menu { get; set; }

        [FromQuery]
        public string? Address { get; set; }

        [FromQuery]
        public string? City { get; set; }

        [FromQuery]
        public string? Country { get; set; }

        [FromQuery]
        public string? BusinessRegNo { get; set; }

        [FromQuery]
        public string? PostalCode { get; set; }

        [FromQuery]
        public string? SortColumn { get; set; }

        [FromQuery]
        public string? SortDirection { get; set; }

        [FromQuery]
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet(string view)
        {
            ViewName = string.IsNullOrEmpty(view) ? "_Dashboard" : view;

            if (view == "_RolesPermissions")
            {
                Roles = _userRepository.GetRoles();
                Permissions = _userRepository.GetPermissions();
            }

            if (view == "_Restaurants")
            {
                Restaurants = _restaurantRepository.GetRestaurants(
                    PageIndex,
                    10,
                    Name,
                    Menu,
                    Address,
                    City,
                    Country,
                    BusinessRegNo,
                    PostalCode,
                    SortColumn,
                    SortDirection
                );
            }

            if(view == "_Clients")
            {
                RestaurantOwners = _userRepository.GetRestaurantOwners(
                    PageIndex,
                    10,
                    FirstName,
                    LastName,
                    Username,
                    JoinDate,
                    EndDate,
                    SortColumn,
                    SortDirection);
            }

            if (view == "_SystemUsers")
            {
                SystemUsers = _userRepository.GetSystemUsers(
                    PageIndex,
                    10,
                    FirstName,
                    LastName,
                    Username,
                    JoinDate,
                    EndDate,
                    SortColumn,
                    SortDirection);
            }

            return Page();
        }
    }
}