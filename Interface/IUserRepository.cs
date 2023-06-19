using Product_management.Models;

namespace Product_management.Interface
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        ICollection<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
      
    }
}
