using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Product_management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IOrderRepository _orderRepository;
        private DataContext _context;
        public HomeController(ILogger<HomeController> logger,IOrderRepository   orderRepository,DataContext context)
        {
            _logger = logger;
            _orderRepository = orderRepository; 
            _context = context;

        }

        public IActionResult Index()
        {
        //    var order = ordersInCurrentMonth.Select(x => new { x.Id, x.OrderDetails.Select(x2 => x2.OrderId)   }) ;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}