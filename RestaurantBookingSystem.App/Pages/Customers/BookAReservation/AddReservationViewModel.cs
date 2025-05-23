namespace RestaurantBookingSystem.App.Pages.Customers.BookAReservation
{
    public class AddReservationViewModel
    {
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
