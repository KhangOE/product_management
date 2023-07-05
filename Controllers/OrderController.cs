using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Product_management.Interface.Service;
using Product_management.Models;
using Product_management.ModelsTest;
using Product_management.ModelView;
using Product_management.unitOfWork;
namespace Product_management.Controllers
{
    public class OrderController : Controller
    {
        // GET: HomeController1
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderServicecs _orderServicecs;
        public ISer1 _ser1;
        public IS2 _ser2;
        public IS3 _ser3;
        public OrderController(IUnitOfWork unitOfWork,IS2 ser2, ISer1 ser1, IS3 ser3,IOrderServicecs orderServicecs)
        {
            _unitOfWork = unitOfWork;
            _orderServicecs = orderServicecs;   
            _ser1 = ser1;
            _ser2 = ser2;
            _ser3 = ser3;
        }
        public async Task<IActionResult> Index()
        {
         //   var service = new ServiceCollection();
           // service.AddSingleton<IOrderRepository, OrderRepository>();
            //var provider = service.BuildServiceProvider();
            // var o = provider.GetRequiredService<IOrderRepository>();
            var orders = await  _unitOfWork.OrderRepository.GetAll();
            var HighstOrder = await _unitOfWork.OrderRepository.GetHighestAmountOrder();
           
            var modelView =   new OrderViewModel()
            {
                ser1 = _ser1.getRandomNumber(),
                ser2 = _ser2.getRandomNumber(),
                ser3 = _ser3.getRandomNumber(),
               orders =  orders.ToList(),
               HighestOrder = HighstOrder,       
            };
            return View(modelView);
          
          
        }

        public IActionResult Create()
        {
            return View(new 
            {
              
                ser2 = _ser2.getRandomNumber(),
                ser3 = _ser3.getRandomNumber(),
                ser1 = _ser1.getRandomNumber()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel createOrderViewModel)
        {
            try
            {
                var order = new Order()
                {
                    UserId = createOrderViewModel.UserId,
                    Total = createOrderViewModel.TotalAmount,
                };
                await _orderServicecs.Create(createOrderViewModel.UserId,createOrderViewModel.TotalAmount, createOrderViewModel.OrderItem);
            //   await _unitOfWork.OrderRepository.CreateOrder(order, createOrderViewModel.OrderItem);
             //   await _ser
               return RedirectToAction(nameof(Index)); 
            }
            catch
            {
               // transaction.Rollback();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
               // var order = await _unitOfWork.OrderRepository.GetOrder(id);
               var order = await _orderServicecs.GetOrder(id);
                if (order != null)
                {
                    //await _unitOfWork.OrderRepository.DeleteOrder(order);
                    await _orderServicecs.Delete(order);
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
