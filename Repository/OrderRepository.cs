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
            return _dataContext.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .ToList();
        }

        public Order GetOrder(int id)
        {
            return _dataContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void CreateOrder(Order order,List<OrderItemViewModel> orderItemViewModels)

        {
            //    var order = new Order();

            foreach (var i in orderItemViewModels)
            {
                var orderDetail = new OrderDetail()
                {
                    Order = order,
                    ProductId = i.ProductId,
                   // Product = Product,
                    TotalPrice = i.TotalPrice,
                    UnitPrice = i.UnitPrice,
                    quantity = i.Quantity,
                };
                _dataContext.Add(orderDetail);
            }

            // _dataContext.Add()
            _dataContext.Add(order);
            _dataContext.SaveChanges();
        }
        
    }
}
