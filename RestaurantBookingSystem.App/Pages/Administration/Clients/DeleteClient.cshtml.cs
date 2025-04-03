using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;


namespace RestaurantBookingSystem.App.Pages.Administration.Clients
{
    public class DeleteClientModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public RestaurantUser RestaurantOwnerToDelete { get; set; }

        public DeleteClientModel(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
           
        }

        public IActionResult OnGet(int id)
        {
            RestaurantOwnerToDelete = _userRepository.GetRestaurantUserById(id);
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            _userRepository.DeleteRestaurantUser(id);

            return RedirectToPage("/Administration/Index", new { view = "_Clients" });
        }
    }
}
