using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
                _dataContext = dataContext;
        }
        public ICollection<Order> GetAll()
        {
            return _dataContext.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return _dataContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public bool CreateOrder(Order order,User user, List<OrderItemViewModel> orderItemViewModels)

        {
            //    var order = new Order();
            
            foreach (var item in orderItemViewModels)
            {
                var OrderDetail = new OrderDetail() {
                    ProductId = item.ProductId,
                    quantity = item.Quantity,
                    Order = order,
                };

                _dataContext.Add(OrderDetail);
            }

            order.User = user;
            _dataContext.Orders.Add(order);
            

            return Save();
        }
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
