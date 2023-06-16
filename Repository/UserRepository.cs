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
            return _dataContext.Users.ToList(); 
        }
        public User GetUserById(int id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool DeleteUser(User user)
        {
            _dataContext.Users.Remove(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _dataContext.Update(user);
            return Save();
        }


        public bool CreateUser(User user)
        {
            _dataContext.Add(user);
            return Save();

        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
