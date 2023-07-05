using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using System.Collections.Specialized;
using System.Transactions;
using Product_management.ModelsTest;
using Microsoft.Extensions.Options;
using Product_management.ModelView;
using Product_management.unitOfWork;
//using System.Diagnostics;

namespace Product_management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;

        private IOrderRepository _orderRepository;
        private IS2 _serv2;
        private IS3 _serv3;
        private ISer1 _ser1;
        private IS2 _serv22;
        private DataContext _context;
        private int age;
        private string name;
        public HomeController(IUnitOfWork unitOfWork,IOptions<Class> option,IS2 s22,IS2 s2,IS3 s3,ISer1 ser1,ILogger<HomeController> logger,IOrderRepository   orderRepository,DataContext context)
        {
            _logger = logger;
            _orderRepository = orderRepository; 
            _context = context;
            _ser1 = ser1;
            _serv3 = s3;
            _serv2 = s2;
            _serv22 = s22;
            var _option = option.Value;
            age = _option.age; 
            name = _option.name;
            _unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> Index()
        {

            var user = await _unitOfWork.UserRepository.GetUserById(1);
        //    var order = ordersInCurrentMonth.Select(x => new { x.Id, x.OrderDetails.Select(x2 => x2.OrderId)   }) ;
            return View(new
            {
                age = age,
                name = name,
                s1 = _ser1.getRandomNumber(),
                s2 =_serv2.getRandomNumber(),
                s3 = _serv3.getRandomNumber(),
                s22 = _serv22.getRandomNumber(),
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}