using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Reservations
{
    public class AddEditReservationViewModel
    {
        public int RestaurantId { get; set; }
        public int? ReservationId { get; set; }
        public int? SelectedCustomerId { get; set; }
        public int? SelectedEmployeeId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }
        public int SelectedReservationStatusId { get; set; }
        public int SelectedTableId { get; set; }
        public int SelectedOrderStatusId { get; set; }

        public List<SelectListItem> ReservationStatuses { get; set; } = new();
        public List<SelectListItem> OrderStatuses { get; set; } = new();
        public List<SelectListItem> AvailableTables { get; set; } = new();
        public List<SelectListItem> Employees { get; set; } = new();
    }
}
