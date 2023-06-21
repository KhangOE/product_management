using Product_management.Models;

namespace Product_management.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);

        
        Task<ICollection<User>> GetUsers();
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
      
    }
}
