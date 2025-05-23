using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Services.BusinessLogic.Interfaces;

public interface IUserService
{
    string GenerateToken(IEnumerable<Claim> claims);

    (bool IsValid, string UserType) IsTokenValid(string token);

    (string Token, string UserType, int RestaurantId) CreateTokenForUserWithRole(string username, string password);

    public string CreateTokenForCustomer(string username, string password);

}
