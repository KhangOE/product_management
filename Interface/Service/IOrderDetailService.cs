using Product_management.Models;

namespace Product_management.Interface.Service
{
    public interface IOrderDetailService
    {
        Task Create(OrderDetail orderDetail);
    }
}
