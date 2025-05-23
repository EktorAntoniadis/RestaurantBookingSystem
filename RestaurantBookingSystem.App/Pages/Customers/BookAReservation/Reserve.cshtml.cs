using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using System.Security.Claims;

namespace RestaurantBookingSystem.App.Pages.Customers.BookAReservation
{
    public class ReserveModel : PageModel
    {
        private readonly IRestaurantRepository _restauranRepository;
        public ReserveModel(IRestaurantRepository restauranRepository)
        {
            _restauranRepository = restauranRepository ?? throw new ArgumentNullException(nameof(restauranRepository));
        }


        [BindProperty]
        public AddReservationViewModel AddReservation { get; set; }

        public IActionResult OnGet(int id)
        {
            var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = User.FindFirst(ClaimTypes.Surname)?.Value;
            var phoneNumer = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            AddReservation = new AddReservationViewModel
            {
                RestaurantId = id,
                CustomerId = customerId,
                CustomerFirstName = firstName,
                CustomerLastName = lastName,
                CustomerPhoneNumber = phoneNumer,
                CustomerEmail = email,
            };
            return Page();
        }

        public IActionResult OnPostAddNewReservation()
        {
            var reservationStatuses = _restauranRepository.GetReservationStatuses();
            var orderStatuses = _restauranRepository.GetOrderStatuses();
            var table = new Table() { TableName = "", Status = new TableStatus() };

            var pendingReservationStatus = reservationStatuses.FirstOrDefault(x => x.Status == "Pending");
            var pendingOrderStatus = orderStatuses.FirstOrDefault(x => x.Status == "Pending");

            var tables = _restauranRepository.GetAvailableTables(AddReservation.RestaurantId,
                AddReservation.ReservationDate,
                AddReservation.ReservationTime,
                AddReservation.NumberOfPeople);

            if (tables.Any())
            {
                table = tables.FirstOrDefault();
            }

            var reservation = new Reservation
            {
                CustomerId = AddReservation.CustomerId,
                RestaurantId = AddReservation.RestaurantId,
                NumberOfPeople = AddReservation.NumberOfPeople,
                ReservationDate = AddReservation.ReservationDate,
                ReservationTime = AddReservation.ReservationTime,
                ReservationStatusId = pendingReservationStatus.Id,
                TableOrder = new TableOrder()
                {
                    TableId = table.Id,
                    OrderStatusId = pendingOrderStatus.Id,
                }
            };

            _restauranRepository.AddReservation(reservation);

            return RedirectToPage("/Customers/Index", new { view = "_Reservations" });
        }
    }
}
