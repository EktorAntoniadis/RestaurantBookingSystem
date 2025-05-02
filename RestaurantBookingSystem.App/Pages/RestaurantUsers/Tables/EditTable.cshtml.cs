using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Tables
{
    public class EditTableModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private int RestaurantId;

        public EditTableModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        [BindProperty]
        public Table EditTable { get; set; }

        public List<TableStatus> TableStatuses { get; set; }

        public IActionResult OnGet(int id)
        {
            var restaurantIdClaim = User.FindFirst("RestaurantId");
            RestaurantId = int.Parse(restaurantIdClaim.Value);
            TableStatuses = _restaurantRepository.GetTableStatuses();
            var statusAvailable = TableStatuses.FirstOrDefault(x => x.Status == "Available");
            EditTable = _restaurantRepository.GetTableById(id);

            return Page();
        }

        public IActionResult OnPostEditCurrentTable()
        {
            ModelState.Remove("EditTable.Status");
            ModelState.Remove("EditTable.Restaurant");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _restaurantRepository.UpdateTable(EditTable);           

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Tables" });
        }
    }
}
