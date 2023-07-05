using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ICollection<User>> GetUsers() {
            return await _dataContext.Users.Include(x => x.Orders).ToListAsync(); 
        }
        public async Task<User> GetUserById(int id)
        {
            return await _dataContext.Users.Include(x => x.Orders).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteUser(User user)
        {
            _dataContext.Users.Remove(user);
        }

        public async Task UpdateUser(User user)
        {
            _dataContext.Update(user);
           
        }

        public async Task CreateUser(User user)
        {
            _dataContext.Add(user);
        }

    }
}
