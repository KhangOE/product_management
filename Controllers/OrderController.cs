using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;

namespace Product_management.Controllers
{
    public class OrderController : Controller
    {
        // GET: HomeController1
       
        private readonly IUnitOfWork _unitOfWork;
        

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {

            List<Order> orders = _unitOfWork.OrderRepository.GetAll().ToList();

            var currentTime = DateTime.Now;

            //  highest value of total order
            int HighestTotalAmoutvalue = !orders
                .IsNullOrEmpty() ? orders
                .Where(x => x.CreateDate.Month == currentTime.Month && x.CreateDate.Year == currentTime.Year)
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
          
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel createOrderViewModel)
        {
            try
            {

                var order = new Order()
                {
                    UserId = createOrderViewModel.UserId,
                    Total = createOrderViewModel.TotalAmount,
                };
              
               _unitOfWork.OrderRepository.CreateOrder(order, createOrderViewModel.OrderItem);
               _unitOfWork.Save();
             
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                return View();
            }
        }

       
    }
}
