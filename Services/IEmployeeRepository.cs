using RestaurantBookingSystem.Models;

namespace RestaurantBookingSystem.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllRoles();
        Role? GetRoleById(int id);

        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }
}
