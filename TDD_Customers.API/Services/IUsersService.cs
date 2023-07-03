using TDD_Customers.API.Models;

namespace TDD_Customers.API.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }
}
