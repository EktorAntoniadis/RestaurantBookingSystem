using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.App.Pages.RestaurantUsers.Tables
{
    public class AddTableModel : PageModel
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private int RestaurantId;

        public AddTableModel(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        }

        [BindProperty]
        public Table AddTable { get; set; }

        public List<TableStatus> TableStatuses { get; set; }

        public IActionResult OnGet()
        {
            var restaurantIdClaim = User.FindFirst("RestaurantId");
            RestaurantId = int.Parse(restaurantIdClaim.Value);
            TableStatuses = _restaurantRepository.GetTableStatuses();
            var statusAvailable = TableStatuses.FirstOrDefault(x => x.Status == "Available");

            AddTable = new Table()
            {
                TableName = string.Empty,
                Status = statusAvailable,
                TableStatusId = statusAvailable.Id,
                RestaurantId = RestaurantId
            };

            return Page();
        }

        public IActionResult OnPostAddNewTable()
        {
            ModelState.Remove("AddTable.Status");
            ModelState.Remove("AddTable.Restaurant");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _restaurantRepository.AddTable(AddTable);

            return RedirectToPage("/RestaurantUsers/Index", new { view = "_Tables" });
        }
    }
}
