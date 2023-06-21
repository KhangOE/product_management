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
        public async Task<ICollection<Order>> GetAll()
        {
            return await _dataContext.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .Include(x => x.User).ToListAsync();    
        }

        // user order nhiều nhất tháng
        public async Task<User> HighestOrderedUser()
        {
            var currentTime = DateTime.Now;
            var users =  _dataContext.Users;
     
            // user has highest number order this month lambda
            var HighestOrderUser = _dataContext.Users.ToList()
                   .MaxBy(x => x.Orders.Count);

           // var maxValue = 
            var query3 = await (from user in users
                          join order in (from order in _dataContext.Orders
                                         where order.CreateDate.Month == currentTime.Month
                                           && order.CreateDate.Year == currentTime.Year
                                         select order)
                          on user.Id equals order.UserId into userGroup
                          where userGroup.Count() == users.Max(x => x.Orders.Count)
                         // orderby userGroup.Count()
                          //descending
                          select user).FirstOrDefaultAsync();
            return  query3;
        }

     

        //sản phẩm được mua nhiều nhất tháng
        public async Task<Product> HighestBoughProduct()
        {
            var currentTime = DateTime.Now;
            var products = _dataContext.Products;
            var orderdetails =  _dataContext.OrderDetails;

            //lambda 
            Product result = products.ToList()
                .MaxBy(p => p.OrderDetails
                    .Where(x => x.Order.CreateDate.Month == currentTime.Month
                        && x.Order.CreateDate.Year == currentTime.Year)
                    .Sum(x => x.quantity));

            //syntax entity
            Product query = await (from product in products
                                   join orderdetail in (from orderDetail in orderdetails
                                                        where orderDetail.Order.CreateDate.Month == currentTime.Month
                                                        && orderDetail.Order.CreateDate.Month == currentTime.Month
                                                        select orderDetail)
                                   on product.Id
                                   equals orderdetail.ProductId into productGroup
                                   orderby productGroup.Sum(x => x.quantity)  descending
                                   select product).FirstOrDefaultAsync();
            return result;
        }

        // order cao nhất tháng này
        public async Task<Order> GetHighestAmountOrder()
        {
            var currentTime = DateTime.Now;
            var orders =_dataContext.Orders;
            // highest Order in labda syntax
            Order HighstOrderLambda =  _dataContext.Orders.ToList()
                .Where(x => x.CreateDate.Month == currentTime.Month 
                    && currentTime.Year == x.CreateDate.Year)
                .MaxBy(x => x.Total);
           
            //in entity query syntax
            Order HighstOrderQueryEntity = await (from order in orders
                                            where order.CreateDate.Month == currentTime.Month 
                                               && order.CreateDate.Year == currentTime.Year
                                               && order.Total == orders.Max(x => x.Total)
                                            //orderby order.Total descending
                                            select order).FirstOrDefaultAsync();
            return HighstOrderQueryEntity;
        }
        public async Task DeleteOrder(Order order)
        {
             _dataContext.Remove(order);
        }
        public async Task<Order> GetOrder(int id)
        {
            return await  _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateOrder(Order order,List<OrderItemViewModel> orderItemViewModels)
        {
            foreach (var i in orderItemViewModels)
            {
                var orderDetail = new OrderDetail()
                {
                    Order = order,
                    ProductId = i.ProductId,
                    TotalPrice = i.TotalPrice,
                    UnitPrice = i.UnitPrice,
                    quantity = i.Quantity,
                };
                  _dataContext.AddAsync(orderDetail);
            }
            _dataContext.Add(order);
            await _dataContext.SaveChangesAsync();
        }
        
    }
}
