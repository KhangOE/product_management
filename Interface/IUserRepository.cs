using Product_management.Models;

namespace Product_management.Interface
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        ICollection<User> GetUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
