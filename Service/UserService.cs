using Microsoft.VisualBasic;
using Product_management.Interface.Service;
using Product_management.Models;
using Product_management.unitOfWork;

namespace Product_management.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<User>> GetUsers()
        {
            return await _unitOfWork.UserRepository.GetUsers();
        }

        public async Task Create(User user)
        {
            await _unitOfWork.UserRepository.CreateUser(user);
        }

        public async Task SavingProduct(int userId,int productId)
        {
            try
            {
               
                var user =await _unitOfWork.UserRepository.GetUserById(userId);
                var product =await _unitOfWork.ProductRepository.GetProductById(productId);

               // Console.WriteLine("user service u"+user2.Name+"p" + product2.Name);
                
                   if (user.ProductsSaved.ContainsKey(product.Id))
                   {
                       Console.WriteLine("remove");
                       user.ProductsSaved.Remove(product.Id);
                   }
                   else
                   {
                       Console.WriteLine("add");
                       user.ProductsSaved.Add(product.Id, product);
                   }
                   await _unitOfWork.UserRepository.UpdateUser(user);
                   await _unitOfWork.SaveChangesAsync();
            }
            catch {
                throw new Exception();
            }
        }
    }
}
