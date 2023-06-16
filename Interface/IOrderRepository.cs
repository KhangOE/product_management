using Microsoft.EntityFrameworkCore;
using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Interface
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAll();
        Order GetOrder(int id);
        bool CreateOrder(Order order,User user, List<OrderItemViewModel> orderItem);
        bool Save();
    }
}
