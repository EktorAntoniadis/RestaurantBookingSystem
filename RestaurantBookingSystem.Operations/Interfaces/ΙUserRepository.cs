using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Pagination;

namespace RestaurantBookingSystem.Operations.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddPermission(Permission permission);
        Permission? GetPermissionById(int id);
        IEnumerable<Permission> GetPermissions();      
        void UpdatePermission(Permission permission);
        void DeletePermission(int id);
        void AddRole(Role role);
        Role? GetRoleById(int id);
        IEnumerable<Role> GetRoles();
        void UpdateRole(Role role, List<int> selectedPermissionIds);
        void DeleteRole(int id);
        void AddRestaurantUser(RestaurantUser user);
        RestaurantUser? GetRestaurantUserById(int id);
        PaginatedList<RestaurantUser> GetRestaurantOwners(
            int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? username = null,
            DateTime? joinDate = null,
            DateTime? endDate = null,
            string? sortColumn = "firstName",
            string? sortDirection = "asc");
        void UpdateRestaurantUser(RestaurantUser user);
        void DeleteRestaurantUser(int id);
        void AddSystemUser(SystemUser user);
        SystemUser? GetSystemUserById(int id);
        void UpdateSystemUser(SystemUser user);
        void DeleteSystemUser(int id);

        PaginatedList<SystemUser> GetSystemUsers(
            int pageIndex,
            int pageSize,
            string? firstName = null,
            string? lastName = null,
            string? username = null,
            DateTime? joinDate = null,
            DateTime? endDate = null,
            string? sortColumn = "firstName",
            string? sortDirection = "asc");

        SystemUser? GetSystemUserByUsername(string username);

        RestaurantUser? GetRestaurantUserByUsername(string username);
        public IEnumerable<RestaurantUser> GetEmployeesByRestaurant(int restaurantId);
        public Customer? FindCustomer(Customer customer);
    }
}
