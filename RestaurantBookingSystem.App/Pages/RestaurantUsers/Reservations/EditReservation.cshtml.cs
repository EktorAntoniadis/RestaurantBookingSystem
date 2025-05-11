using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Reservations
{
    public class EditReservationModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRestaurantService _restaurantService;

        public EditReservationModel(
            IRestaurantRepository restaurantRepository,
            IUserRepository userRepository,
            IRestaurantService restaurantService)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        }

        [BindProperty]
        public AddEditReservationViewModel EditReservationViewModel { get; set; }
        public string ErrorMessage { get; set; }
        public IActionResult OnGet(int id)
        {
            var restaurantIdClaim = User.FindFirst("RestaurantId");
            var restauranId = int.Parse(restaurantIdClaim.Value);
            var reservation = _restaurantRepository.GetReservationById(id);
            var reservationStatuses = _restaurantRepository.GetReservationStatuses();
            var availableTables = _restaurantRepository.GetAllTables(restauranId);
            var orderStatuses = _restaurantRepository.GetOrderStatuses();
            var employees = _userRepository.GetEmployeesByRestaurant(restauranId);

            var waiters = employees.Where(x=>x.Role.Name == "Waiter").ToList();

            EditReservationViewModel = new AddEditReservationViewModel
            {
                RestaurantId = restauranId,
                ReservationDate = reservation.ReservationDate,
                ReservationTime = reservation.ReservationTime,
                SelectedCustomerId = reservation.CustomerId,
                NumberOfPeople = reservation.NumberOfPeople,
                CustomerFirstName  =reservation.Customer.FirstName,
                CustomerLastName =reservation.Customer.LastName,
                CustomerPhoneNumber =reservation.Customer.Phone,
                CustomerEmail = reservation.Customer.Email,
                SelectedOrderStatusId = reservation.TableOrder.OrderStatusId,
                SelectedReservationStatusId = reservation.ReservationStatusId,
                SelectedTableId = reservation.TableOrder.TableId,
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
                Employees = waiters.Select(x=> new SelectListItem
                {
                    Text = $"{x.FirstName} {x.LastName}",
                    Value = x.Id.ToString()
                }).ToList()
            };
            return Page();
        }

        public IActionResult OnPostUpdateReservation(int id)
        {
            var currentReservation = _restaurantRepository.GetReservationById(id);

            currentReservation.ReservationDate = EditReservationViewModel.ReservationDate;
            currentReservation.ReservationTime = EditReservationViewModel.ReservationTime;
            currentReservation.ReservationStatusId = EditReservationViewModel.SelectedReservationStatusId;
            currentReservation.NumberOfPeople = EditReservationViewModel.NumberOfPeople;
            currentReservation.TableOrder.OrderStatusId = EditReservationViewModel.SelectedOrderStatusId;
            currentReservation.TableOrder.TableId = EditReservationViewModel.SelectedTableId;
            currentReservation.EmployeeId = EditReservationViewModel.SelectedEmployeeId!.Value;

            _restaurantRepository.UpdateReservation(currentReservation);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Reservations" });
        }
    }
}
