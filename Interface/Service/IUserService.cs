using Product_management.Models;

namespace Product_management.Interface.Service
{
    public interface IUserService
    {
      //  Dictionary
        Task<ICollection<User>> GetUsers();

        Task Create(User user);
      
        Task SavingProduct(int userId,int productId);
    }
}
