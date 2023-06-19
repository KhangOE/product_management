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

        public  ICollection<User> GetUsers() {
            return _dataContext.Users.Include(x => x.Orders).ToList(); 
        }
        public User GetUserById(int id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void DeleteUser(User user)
        {
            _dataContext.Users.Remove(user);
           
        }

        public void UpdateUser(User user)
        {
            _dataContext.Update(user);
           
        }


        public void CreateUser(User user)
        {
            _dataContext.Add(user);
        }

    }
}
