using RestaurantBookingSystem.Models;

namespace RestaurantBookingSystem.Services.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private RestaurantBookingSystemDbContext _dbContext;

        public EmployeeRepository(RestaurantBookingSystemDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Role? GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
