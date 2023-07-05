using Microsoft.EntityFrameworkCore;
using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Interface
{
    public interface IOrderRepository
    {
        Task<ICollection<Order>> GetAll();
        Task<Order> GetHighestAmountOrder();
        Task<Product> HighestBoughProduct();
        Task<List<Product>> TopTenBoughProduct();
        Task<User> HighestOrderedUser();
        Task<Order> GetOrder(int id);
        Task DeleteOrder(Order order);
        Task CreateOrder(Order order);
       
    }
}
