using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Transactions;

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
            var users = _dataContext.Users.ToList();
            var orders = _dataContext.Orders;

            // user has highest number order this month lambda
            var HighestOrderUser = _dataContext.Users.ToList()
                   .MaxBy(x => x.Orders.Count);

            
            // var maxValue = 
            var query3 = (from user in users.AsParallel()
                          join order in orders.AsParallel()
                          on user.Id equals order.UserId into userGroup
                          from ug in userGroup.DefaultIfEmpty()
                          let maxValue = users.Max(x => x.Orders.Where(y => y.CreateDate.Month == currentTime.Month).Count())
                          where  ug?.CreateDate.Month == currentTime.Month &&
                          userGroup.Count() == maxValue
                          // where  userGroup.Count() == users.Max(x => x.Orders.Count)
                          //orderby user.Id
                          select user).FirstOrDefault();
            return query3;
        }

        public async Task<List<Product>> TopTenBoughProduct(){

            var products =  _dataContext.Products;
            var orderdetails =  _dataContext.OrderDetails;

            var lambda = products.OrderByDescending(p => p.OrderDetails
                                    .Sum(x => x.quantity))
                                 .Take(10)
                                 .ToList();
                                 
            var orders = _dataContext .Orders;
            var query =  (from product in products.AsParallel()
                         join orderDetail in orderdetails 
                         on product.Id equals orderDetail.ProductId into productGroup
                         orderby productGroup.Sum(x => x.quantity) descending
                         select product).Take(10).ToList();

            /*  var queryj = await (from product in products
                                  join orderDetail in orderdetails
                                  on product.Id equals orderDetail.ProductId into productGroup
                                  join order in orders on or

                                 from p in productGroup.DefaultIfEmpty()

                                );*/

         
            var queryt = (from product in products
                          orderby product.OrderDetails.Sum(x => x.quantity) descending
                          select product).FirstOrDefault(); 

            return query;

        }

        //sản phẩm được mua nhiều nhất tháng
        public async Task<Product> HighestBoughProduct()
        {
            var currentTime = DateTime.Now;
            var products = await _dataContext.Products.ToListAsync();
            var orderdetails =  _dataContext.OrderDetails;

            //lambda 
            Product result = products
                .AsParallel()
                .MaxBy(p => p.OrderDetails
                    .Where(x => x.Order.CreateDate.Month == currentTime.Month
                        && x.Order.CreateDate.Year == currentTime.Year)
                    .Sum(x => x.quantity));

            //syntax entity
            Product query = (from product in products.AsParallel()
                             join orderdetail in orderdetails.AsParallel()
                             on product.Id
                             equals orderdetail.ProductId into productGroup
                             from pG in productGroup.DefaultIfEmpty()
                             let maxValue = products.Max(x => x.OrderDetails.Where(y => y.Order.CreateDate.Month == currentTime.Month).Sum(y => y.quantity))
                             where pG?.Order.CreateDate.Month == currentTime.Month
                                   && productGroup.Sum(x => x.quantity) == maxValue
                             select product).FirstOrDefault();
            return query;
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
            using var transaction = _dataContext.Database.BeginTransaction();
            try
            {
                _dataContext.Orders.Add(order);
                _dataContext.SaveChanges();
                Console.WriteLine("start");
                object lockObject = new object();
                Parallel.ForEach(orderItemViewModels
                                ,() => new OrderDetail()
                                ,(od, ct, OdCr) =>
                {
                     Console.WriteLine(od.Quantity);
                     OdCr.TotalPrice = od.TotalPrice;
                     OdCr.UnitPrice = od.UnitPrice;
                     OdCr.ProductId = od.ProductId;
                     OdCr.Order = order;
                     OdCr.quantity = od.Quantity;
                     //var odtest = new OrderDetail();
                     return OdCr;
                     //return odtest;
                     /*return new OrderDetail()
                     {
                       TotalPrice = od.TotalPrice,
                       UnitPrice = od.UnitPrice,
                        ProductId = od.ProductId,
                        Order = order,
                      quantity = od.Quantity,
                      };*/
                     /*
                     {
                         TotalPrice = od.TotalPrice,
                         UnitPrice = od.UnitPrice,
                         ProductId = od.UnitPrice,
                         Order = order,
                         quantity = od.Quantity,
                     };*/

                 },  
                  (od) => {
                     
                        lock(lockObject) {
                         Console.WriteLine("add here");
                         Console.WriteLine(od.quantity);
                         _dataContext.OrderDetails.Add(od);
                     }
                 });
                Console.WriteLine("end");

               /*   foreach(var od in orderItemViewModels)
                  {
                      var orderDetail = new OrderDetail()
                      {
                          Order = order,
                          ProductId = od.ProductId,
                          TotalPrice = od.TotalPrice,
                          UnitPrice = od.UnitPrice,
                          quantity = od.Quantity,
                      };
                       _dataContext.Add(orderDetail);
                  }  */
                _dataContext.SaveChanges();
               transaction.Commit(); 
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                transaction.Rollback();
            }
           
         
        }
        
    }
}
