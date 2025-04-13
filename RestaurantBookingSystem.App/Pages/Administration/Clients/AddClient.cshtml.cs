using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Clients;

public class AddClientModel : PageModel
{
    [BindProperty]
    public RestaurantUser RestaurantOwner { get; set; }

    private readonly IUserRepository _userRepository;

    public AddClientModel(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        RestaurantOwner = new RestaurantUser();
    }

    public IActionResult OnPostAddNewClient()
    {
        var roles = _userRepository.GetRoles();

        var businessOwnerRole = roles.FirstOrDefault(x=>x.Name == "Business Owner");

        RestaurantOwner.Role = businessOwnerRole!;
        RestaurantOwner.JoinDate = DateTime.Now;
        RestaurantOwner.EndDate = null;

        _userRepository.AddRestaurantUser(RestaurantOwner);

        return RedirectToPage("/Administration/Index", new { view = "_Clients" });
    }
}
