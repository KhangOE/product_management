using Product_management.Models;

namespace Product_management.Interface
{
    public interface IOrderIDetailRepositorycs
    {
        Task<List<OrderDetail>> GetAll();
        Task CreateOrderDetail(OrderDetail orderDetail);

       // int getRandomNumber();
    }
}
