using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;

namespace RestaurantBookingSystem.App.Pages.Administration.Clients;

public class AddClientModel : PageModel
{

    private readonly IUserRepository _userRepository;
    private PasswordHasher<RestaurantUser> _passwordHasher;

    public AddClientModel(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        RestaurantOwner = new RestaurantUser();
        _passwordHasher = new PasswordHasher<RestaurantUser>();
    }


    [BindProperty]
    public RestaurantUser RestaurantOwner { get; set; }

  

    public IActionResult OnPostAddNewClient()
    {
        var roles = _userRepository.GetRoles();

        var businessOwnerRole = roles.FirstOrDefault(x=>x.Name == "Business Owner");

        RestaurantOwner.Role = businessOwnerRole!;
        RestaurantOwner.JoinDate = DateTime.Now;
        RestaurantOwner.EndDate = null;
        RestaurantOwner.Password = _passwordHasher.HashPassword(RestaurantOwner, RestaurantOwner.Password);

        _userRepository.AddRestaurantUser(RestaurantOwner);

        return RedirectToPage("/Administration/Index", new { view = "_Clients" });
    }
}
