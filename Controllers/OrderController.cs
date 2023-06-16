using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Product_management.Controllers
{
    public class OrderController : Controller
    {
        // GET: HomeController1
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private DataContext _dataContext;

        public OrderController(DataContext dataContext,IOrderRepository orderRepository,IUserRepository userRepository,IProductRepository productRepository)
        {
                _orderRepository = orderRepository;
            _userRepository = userRepository;
                _productRepository = productRepository;
            _dataContext = dataContext;
        }
        public ActionResult Index()
        {

            var orders = _orderRepository.GetAll().ToList();

           // var orderdail3 = _orderRepository.GetAll().ToList().Select(x => x.OrderDetails.ToList()).ToList();
            // var orderdetail = _dataContext.OrderDetails.Select(x => x.Product).ToList();
            //  List<int> ordersum = _orderRepository.GetAll().ToList().Select(x =>  x.OrderDetails.Select(y => y.Product.Price).Sum()).ToList();
            var oderdetail = _dataContext.OrderDetails.Where(x => x.OrderId == 1).Select(x =>  x.Product.Price ).ToList();


            int Total(int id)
            {
                var oderdetail = _dataContext.OrderDetails.Where(x => x.OrderId == id).Select(x => x.Product.Price * x.quantity).ToList().Sum();
                return oderdetail;
            }


            var groupedOrders = _dataContext.OrderDetails
                .GroupBy(od => od.OrderId)
                .Select(g => new { OrderId = g.Key, TotalAmount = g.Sum(od => od.Product.Price) }).Select(x => x.TotalAmount).ToList();


            DateTime currentDateTime = DateTime.Now;
            int currentMonth = currentDateTime.Month;

            int highestOrder = orders.Count != 0 ? orders
           .Where(o => o.CreateDate.Month == currentMonth)
           .OrderByDescending(o => Total(o.Id))
           .FirstOrDefault().Id : -1;


            int highestUser = _dataContext.Orders
         .Where(o => o.CreateDate.Month == currentMonth).Select(x => x.UserId)
         .GroupBy(n => n)
         .Select(g => new { Number = g.Key, Count = g.Count() })
         .OrderByDescending(g => g.Count).Select(x => x.Number)
         .FirstOrDefault();
            int highestUserTotal = _dataContext.Orders
         .Where(o => o.CreateDate.Month == currentMonth).Select(x => x.UserId)
         .GroupBy(n => n)
         .Select(g => new { Number = g.Key, Count = g.Count() })
         .OrderByDescending(g => g.Count).Select(x => x.Count)
         .FirstOrDefault();


            List<OrderViewModel> ordersViewModel = orders.Select(
            x => new OrderViewModel()
            {
                highestUserTotal = highestUserTotal,
                IdH = highestOrder,
                UserId = x.UserId,
                id = x.Id,
                date = x.CreateDate,
                Total = Total(x.Id),
                ProductName = groupedOrders,
                HUserId = highestUser,
                OrderDetails = _dataContext.OrderDetails.Where(y=> y.OrderId == x.Id )
                .Select(x => new OrderDetailViewModel { Name = x.Product.Name , price = x.Product.Price * x.quantity,
                 quantity = x.quantity}).ToList(),
               // Details = orderdail3 
            }
            ).ToList();

        


            return View(ordersViewModel);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {

            //List<ProductViewModel> productViewModel = new ProductViewModel();
            //var ProductToSend = _productRepository.GetAll().ToList();

           // OrderViewModel orderViewModel = new OrderViewModel();

           // var orderdetail = _dataContext.OrderDetails.Select(x =>x.Product).ToList();
            
            return View();
            
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel createOrderViewModel)
        {
            try
            {
                
                var user = _userRepository.GetUserById(createOrderViewModel.UserId);
                Order order2 = new Order() { 
                UserId = createOrderViewModel.UserId,
                User = user,
                };


                _orderRepository.CreateOrder(order2, user, createOrderViewModel.OrderItem);
               _dataContext.Database.ExecuteSqlRaw("DELETE FROM Carts");
                _dataContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
