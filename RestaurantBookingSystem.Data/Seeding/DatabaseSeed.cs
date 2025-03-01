using Microsoft.AspNetCore.Identity;
using RestaurantBookingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Data.Seeding
{
    public class DatabaseSeed
    {
        private readonly RestaurantBookingSystemDbContext _context;
        private PasswordHasher<RestaurantUser> _hasher;

        public DatabaseSeed(RestaurantBookingSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hasher = new PasswordHasher<RestaurantUser>();
        }

        public void SeedAdministrationData()
        {
            var permissionManageMultipleRestaurants = new Permission
            {
                Name = "Manage Multiple Restaurants",
                Description = "Permission to manage multiple restaurant locations"
            };

            var permissionManageSingleRestaurant = new Permission
            {
                Name = "Manage Single Restaurant",
                Description = "Permission to manage a single restaurant"
            };

            var permissionManageRestaurantChain = new Permission
            {
                Name = "Manage Restaurant Chain",
                Description = "Permission to manage a chain of restaurants"
            };

            var permissionEmployeeManagement = new Permission
            {
                Name = "Employee Management",
                Description = "Permission to manage restaurant employees"
            };

            var permissionAccessSystemData = new Permission
            {
                Name = "Access System Data",
                Description = "Permission to access system-wide data"
            };

            var permissionAccessRestaurantData = new Permission
            {
                Name = "Access Restaurant Data",
                Description = "Permission to access specific restaurant data"
            };

            var permissionManageCustomerOrders = new Permission
            {
                Name = "Order Management",
                Description = "Permission to manage customer orders"
            };

            var permissionCustomerManagement = new Permission
            {
                Name = "Customer Management",
                Description = "Permission to manage customer information"
            };

            var permissionReservationManagement = new Permission
            {
                Name = "Reservation Management",
                Description = "Permission to manage customers reservations"
            };
            if (!_context.Permissions.Any())
            {

                _context.Permissions.AddRange(
                    permissionManageMultipleRestaurants,
                    permissionManageSingleRestaurant,
                    permissionManageRestaurantChain,
                    permissionEmployeeManagement,
                    permissionAccessSystemData,
                    permissionAccessRestaurantData,
                    permissionManageCustomerOrders,
                    permissionCustomerManagement,
                    permissionReservationManagement
                );

                _context.SaveChanges();
            }


            var roleSuperAdmin = new Role
            {
                Name = "Super Administrator",
                Description = "This is the super administrator role",
                Permissions = new List<Permission>
                {
                    permissionManageMultipleRestaurants,
                    permissionManageSingleRestaurant,
                    permissionManageRestaurantChain,
                    permissionAccessSystemData,
                    permissionAccessRestaurantData
                }
            };

            var roleBusinessOwner = new Role
            {
                Name = "Business Owner",
                Description = "This is the business owner role",
                Permissions = new List<Permission>
                {
                    permissionManageSingleRestaurant,
                    permissionManageRestaurantChain,
                    permissionAccessRestaurantData,
                    permissionEmployeeManagement
                }
            };

            var roleManager = new Role
            {
                Name = "Manager",
                Description = "This is the manager role",
                Permissions = new List<Permission>
                {
                    permissionManageSingleRestaurant,
                    permissionEmployeeManagement,
                    permissionAccessRestaurantData,
                    permissionCustomerManagement
                }
            };

            var roleReceptionist = new Role
            {
                Name = "Receptionist",
                Description = "This is the receptionist role",
                Permissions = new List<Permission>
                {
                    permissionCustomerManagement,
                    permissionReservationManagement
                }
            };

            var roleWaiter = new Role
            {
                Name = "Waiter",
                Description = "This is the waiter role",
                Permissions = new List<Permission>
                {
                    permissionManageCustomerOrders,
                    permissionCustomerManagement
                }
            };

            var roleKitchenStaff = new Role
            {
                Name = "Kitchen Staff",
                Description = "This is the kitchen staff role",
                Permissions = new List<Permission>
                {
                    permissionManageCustomerOrders
                }
            };

            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(roleSuperAdmin, roleBusinessOwner,
                    roleManager, roleReceptionist, roleWaiter, roleKitchenStaff);
                _context.SaveChanges();
            }

            var superAdminEmployee = new RestaurantUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "2107895654",
                Role = roleSuperAdmin,
                Username = "johndoe"
            };

            superAdminEmployee.Password = _hasher.HashPassword(superAdminEmployee, "!superAdmin1234!");

            if (!_context.RestaurantUsers.Any(x => x.Username == "johndoe" && x.Role.Name == "Super Administrator"))
            {
                _context.RestaurantUsers.Add(superAdminEmployee);
                _context.SaveChanges();
            }
        }
    }
}
