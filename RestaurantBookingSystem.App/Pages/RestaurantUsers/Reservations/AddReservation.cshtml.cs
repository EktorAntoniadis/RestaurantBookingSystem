using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Reservations
{
    public class AddReservationModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRestaurantService _restaurantService;

        public AddReservationModel(
            IRestaurantRepository restaurantRepository,
            IUserRepository userRepository,
            IRestaurantService restaurantService)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        }

        [BindProperty]
        public AddEditReservationViewModel AddReservationData { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var restaurantIdClaim = User.FindFirst("RestaurantId");
            var restauranId = int.Parse(restaurantIdClaim.Value);
            var reservationStatuses = _restaurantRepository.GetReservationStatuses();
            var availableTables = _restaurantRepository.GetAllTables(restauranId);
            var orderStatuses = _restaurantRepository.GetOrderStatuses();

            AddReservationData = new AddEditReservationViewModel
            {
                RestaurantId = restauranId,
                ReservationDate = DateOnly.FromDateTime(DateTime.Today),
                ReservationTime = TimeOnly.FromDateTime(DateTime.Now),
                ReservationStatuses = reservationStatuses.Select(x => new SelectListItem
                {
                    Text = x.Status,
                    Value = x.Id.ToString()
                }).ToList(),
                OrderStatuses = orderStatuses.Select(x => new SelectListItem
                {
                    Text = x.Status,
                    Value = x.Id.ToString()
                }).ToList(),
                AvailableTables = availableTables.Select(x => new SelectListItem
                {
                    Text = $"{x.TableName}, {x.NumberOfPeople} people",
                    Value = x.Id.ToString()
                }).ToList(),
            };
            return Page();
        }

        public IActionResult OnPostCreateReservation()
        {
            var availableTables = _restaurantRepository.GetAvailableTables(
               AddReservationData.RestaurantId,
               AddReservationData.ReservationDate,
               AddReservationData.ReservationTime,
               AddReservationData.NumberOfPeople);           

            if(!availableTables.Any())
            {
                ErrorMessage = "No tables available for the specific date and time";
                return Page();
            }

            var orderStatuses = _restaurantRepository.GetOrderStatuses();
            var table = availableTables.FirstOrDefault(x => x.Id == AddReservationData.SelectedTableId);
            var orderStatus = orderStatuses.FirstOrDefault(x => x.Status == "Pending");

            var reservation = new Reservation
            {
                RestaurantId = AddReservationData.RestaurantId,
                Customer = new Customer
                {
                    FirstName = AddReservationData.CustomerFirstName,
                    LastName = AddReservationData.CustomerLastName,
                    Phone = AddReservationData.CustomerPhoneNumber,
                },
                NumberOfPeople = AddReservationData.NumberOfPeople,
                ReservationDate = AddReservationData.ReservationDate,
                ReservationTime = AddReservationData.ReservationTime,
                ReservationStatusId = AddReservationData.SelectedReservationStatusId,
                TableOrder = new TableOrder
                {
                    TableId = table.Id,
                    RestaurantId = AddReservationData.RestaurantId,
                    OrderStatusId = orderStatus.Id,
                }

            };

            _restaurantService.SaveNewResevation(reservation);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Reservations" });
        }
    }   
}
