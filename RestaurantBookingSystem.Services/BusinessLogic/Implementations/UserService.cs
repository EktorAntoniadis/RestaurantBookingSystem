using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantBookingSystem.Services.BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<RestaurantUser> _restaurantUserPasswordHasher;
        private readonly PasswordHasher<SystemUser> _systemUserPasswordHasher;
        private readonly PasswordHasher<Customer> _customerHasher;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _restaurantUserPasswordHasher = new PasswordHasher<RestaurantUser>();
            _systemUserPasswordHasher = new PasswordHasher<SystemUser>();
            _customerHasher = new PasswordHasher<Customer>();
        }
        public (string Token, string UserType, int RestaurantId) CreateTokenForUserWithRole(string username, string password)
        {
            var systemUser = _userRepository.GetSystemUserByUsername(username);

            if (systemUser != null)
            {
                var passwordResult = _systemUserPasswordHasher.VerifyHashedPassword(systemUser, systemUser.Password, password);
                if (passwordResult == PasswordVerificationResult.Success)
                {
                    var claims = SetClaims(systemUser);
                    var jwtToken = GenerateToken(claims);
                    return (jwtToken, "SystemUser", 0);
                }
            }
            else
            {
                var restaurantUser = _userRepository.GetRestaurantUserByUsername(username);
                if (restaurantUser != null)
                {
                    var passwordResult = _restaurantUserPasswordHasher.VerifyHashedPassword(restaurantUser, restaurantUser.Password, password);
                    if (passwordResult == PasswordVerificationResult.Success)
                    {
                        var claims = SetClaims(restaurantUser);
                        var jwtToken = GenerateToken(claims);
                        var restaurantId = restaurantUser.RestaurantId ?? 0;
                        return (jwtToken, "RestaurantUser", restaurantId);
                    }
                }
            }

            return (string.Empty, string.Empty, 0);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"]));

            var audience = _configuration["JwtSettings:Audience"];

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: audience,
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (bool IsValid, string UserType) IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));

            var audience = _configuration["JwtSettings:Audience"];

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = audience,
                    ValidAudiences = [audience],
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userType = claimsPrincipal.FindFirst("UserType")?.Value;
                return (true, userType ?? string.Empty);
            }
            catch (Exception ex)
            {
                return (false, string.Empty);
            }
        }

        private static IEnumerable<Claim> SetClaims(object? user)
        {
            var claims = new List<Claim>();

            if (user is RestaurantUser restaurantUser)
            {
                var restaurantId = restaurantUser.RestaurantId != null ? restaurantUser.RestaurantId.ToString() : "0";
                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, restaurantUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, restaurantUser.Username),
                    new Claim(ClaimTypes.Surname, restaurantUser.LastName),
                    new Claim(ClaimTypes.GivenName, restaurantUser.FirstName),
                    new Claim(ClaimTypes.Email, restaurantUser.Email),
                    new Claim("RestaurantId", restaurantId!),
                    new Claim("Role", restaurantUser.Role.Name),
                    new Claim("UserType", "RestaurantUser")
                };

                foreach (var permission in restaurantUser.Role.Permissions)
                {
                    claims.Add(new Claim("Permission", permission.Name));
                }
            }

            if (user is SystemUser systemUser)
            {               
                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, systemUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, systemUser.Username),
                    new Claim(ClaimTypes.Surname, systemUser.LastName),
                    new Claim(ClaimTypes.GivenName, systemUser.FirstName),
                    new Claim(ClaimTypes.Email, systemUser.Email),
                    new Claim("Role", systemUser.Role.Name),
                    new Claim("UserType", "SystemUser")
                };
            }
           
            return claims;
        }

        public string CreateTokenForCustomer(string username, string password)
        {
            var existingCustomer = _userRepository.GetCustomerByUsername(username);

            if (existingCustomer != null)
            {
                var passwordResult = _customerHasher.VerifyHashedPassword(existingCustomer, existingCustomer.Password, password);
                if (passwordResult == PasswordVerificationResult.Success)
                {
                    var claims = SetClaims(existingCustomer);

                    var jwtToken = GenerateToken(claims);
                    return jwtToken;
                }
            }

            return string.Empty;
        }
    }
}
