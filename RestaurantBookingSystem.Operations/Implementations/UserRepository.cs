using Microsoft.EntityFrameworkCore;
using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using System.Security;

namespace RestaurantBookingSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantBookingSystemDbContext _context;

        public UserRepository(RestaurantBookingSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
        }

        public Permission? GetPermissionById(int id)
        {
            return _context.Permissions.Find(id);
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public void UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
            _context.SaveChanges();
        }

        public void DeletePermission(int id)
        {
            var permission = GetPermissionById(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                _context.SaveChanges();
            }
        }
        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public Role? GetRoleById(int id)
        {
            return _context.Roles
                .Include(x=>x.Permissions)
                .FirstOrDefault(x=>x.Id == id);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles
                .Include(x => x.Permissions).ToList();
        }

        public void UpdateRole(Role role, List<int> selectedPermissionIds)
        {
            var currentRole = GetRoleById(role.Id);

            currentRole.Permissions.Clear();
            _context.Roles.Update(currentRole);
            _context.SaveChanges();

            currentRole.Name = role.Name;
            currentRole.Description = role.Description;
            var permissions = _context.Permissions.Where(x => selectedPermissionIds.Contains(x.Id)).ToList();
            currentRole.Permissions = permissions;
            _context.Roles.Update(currentRole);
            _context.SaveChanges();
        }

        public void DeleteRole(int id)
        {
            var role = GetRoleById(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
        public void AddRestaurantUser(RestaurantUser user)
        {
            _context.RestaurantUsers.Add(user);
            _context.SaveChanges();
        }

        public RestaurantUser? GetRestaurantUserById(int id)
        {
            var restaurantUser = _context.RestaurantUsers
                .Include(x => x.Restaurant)
                .ThenInclude(x => x.Menu)
                .ThenInclude(x => x.FoodCategories)
                .ThenInclude(x => x.FoodItems)
                .Include(x => x.Restaurant)
                .ThenInclude(x => x.RestaurantUsers)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == id);

            return restaurantUser;
        }
        public void UpdateRestaurantUser(RestaurantUser user)
        {
            _context.RestaurantUsers.Update(user);
            _context.SaveChanges();
        }

        public void DeleteRestaurantUser(int id)
        {
            var user = GetRestaurantUserById(id);
            
            if (user != null)
            {
                user.RoleId = 0;
                _context.RestaurantUsers.Remove(user);
                _context.SaveChanges();
            }
        }
        public void AddSystemUser(SystemUser user)
        {
            _context.SystemUsers.Add(user);
            _context.SaveChanges();
        }

        public SystemUser? GetSystemUserById(int id)
        {
            return _context.SystemUsers
                .Include(x=>x.Role)
                .FirstOrDefault(x=>x.Id == id);
        }

        public void UpdateSystemUser(SystemUser user)
        {
            _context.SystemUsers.Update(user);
            _context.SaveChanges();
        }

        public void DeleteSystemUser(int id)
        {
            var user = GetSystemUserById(id);
            if (user != null)
            {
                _context.SystemUsers.Remove(user);
                _context.SaveChanges();
            }
        }
        public PaginatedList<RestaurantUser> GetRestaurantOwners(
            int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? username = null,
            DateTime? joinDate = null,
            DateTime? endDate = null,
            string? sortColumn = "firstName",
            string? sortDirection = "asc")
        {
            var query = _context.RestaurantUsers
                .Include(x => x.Role)
                .Include(x=> x.Restaurant)
                .Where(x => x.Role.Name == "Business Owner")
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(x => x.FirstName!.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x => x.LastName!.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(x => x.Username!.Contains(username));
            }

            if (joinDate != null)
            {
                query = query.Where(x => x.JoinDate.Date == joinDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x => x.EndDate!.Value == endDate.Value.Date);
            }

            switch (sortColumn)
            {
                case "LastName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.LastName) : query.OrderBy(x => x.LastName);
                    break;
                case "Username":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Username) : query.OrderBy(x => x.Username);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                    break;
            }

            var totalRecords = query.Count();
            var restaurantUsers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<RestaurantUser>(restaurantUsers, totalRecords, pageIndex, pageSize);
        }

        public PaginatedList<SystemUser> GetSystemUsers(
            int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? username = null,
            DateTime? joinDate = null,
            DateTime? endDate = null,
            string? sortColumn = "firstName",
            string? sortDirection = "asc")
        {
            var query = _context.SystemUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(x => x.FirstName!.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x => x.LastName!.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(x => x.Username!.Contains(username));
            }

            if (joinDate != null)
            {
                query = query.Where(x => x.JoinDate.Date == joinDate.Value.Date);
            }

            if (endDate != null)
            {
                query = query.Where(x => x.EndDate.Value.Date == endDate.Value.Date);
            }

            switch (sortColumn)
            {
                case "LastName":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.LastName) : query.OrderBy(x => x.LastName);
                    break;
                case "Username":
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.Username) : query.OrderBy(x => x.Username);
                    break;
                default:
                    query = sortDirection == "desc" ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                    break;
            }

            var totalRecords = query.Count();
            var sysUsers = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<SystemUser>(sysUsers, totalRecords, pageIndex, pageSize);
        }

        public SystemUser? GetSystemUserByUsername(string username)
        {
            var sysUser = _context.SystemUsers
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefault(s => s.Username == username);
            return sysUser;
        }

        public RestaurantUser? GetRestaurantUserByUsername(string username)
        {
            var resUser = _context.RestaurantUsers
                .Include(x => x.Role)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefault(x => x.Username == username);
            return resUser;
        }

        public IEnumerable<RestaurantUser> GetEmployeesByRestaurant(int restaurantId)
        {
            var employees = _context.RestaurantUsers
                .Include(x => x.Role)
                .Where(x => x.RestaurantId == restaurantId).ToList();

            return employees;
        }

        public Customer? FindCustomer(Customer customer)
        {
            var existinCustomer = _context.Customers.FirstOrDefault(x => x.FirstName.Contains(customer.FirstName) && x.LastName.Contains(customer.LastName));

            if (existinCustomer == null)
            {
                existinCustomer = _context.Customers.FirstOrDefault(x => x.Phone == customer.Phone);
            }

            return existinCustomer;
        }
    }
}