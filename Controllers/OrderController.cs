using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

            List<Order> orders = _orderRepository.GetAll().ToList();

            var currentTime = DateTime.Now;

            //  highest value of total order
            int HighestTotalAmoutvalue = orders.Count() != 0 ? orders.Where(x => x.CreateDate.Month == currentTime.Month && x.CreateDate.Year == currentTime.Year)
                .Max(x => x.Total) : 0;

            // order has most value total
            Order HighstOrder = orders.Where(x => x.CreateDate.Month == currentTime.Month && currentTime.Year == x.CreateDate.Year)
                .FirstOrDefault(x => x.Total == HighestTotalAmoutvalue);


            var modelView = new OrderViewModel()
            {
                orders = orders,
                HighestOrder = HighstOrder,
        
               
            };
            return View(modelView);
          
          return View();
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

                var order = new Order()
                {
                    
                    User = user,
                    UserId = createOrderViewModel.UserId,
                    Total = createOrderViewModel.TotalAmount,
                };
                var Product = _productRepository.GetProductById(1);

               _orderRepository.CreateOrder(order, createOrderViewModel.OrderItem);
             
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
