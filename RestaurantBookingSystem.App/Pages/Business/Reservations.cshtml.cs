using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace RestaurantBookingSystem.App.Pages.Business
{
    public class ReservationsModel : PageModel
    {
        [BindProperty]
        public Reservation NewReservation { get; set; }

        public List<Reservation> Reservations { get; set; }

        public void OnGet()
        {

            Reservations = new List<Reservation>();

            Reservations.Add(new Reservation { Name = "John Doe", Date = "2025-02-20", Time = "19:00" });
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reservations.Add(NewReservation);
            return RedirectToPage();
        }
    }

    public class Reservation
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
