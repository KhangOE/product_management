using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Interface.Service
{
    public interface IOrderServicecs
    {
        Task<ICollection<Order>> GetOrders();
        Task<Dictionary<int,string>> GetSplierAvailable();
        Task<HashSet<Address>> AdressAvailble(int UserId, HashSet<OrderItemViewModel> orderItemViewModels);
        Task<bool> checkSplier(HashSet<OrderItemViewModel> orderItemViewModels);
        //Task<Order> GetOrder(int id);
        Task<Order> GetOrder(int id);
        Task Delete(Order order);
        Task Create(int UserId, int TotalAmount,List<OrderItemViewModel> orderItemViewModels);


     
    }
}
