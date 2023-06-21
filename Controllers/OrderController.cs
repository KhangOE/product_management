using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public async Task<IActionResult> Index()
        { 
            var orders = await  _unitOfWork.OrderRepository.GetAll();
            var HighstOrder = await _unitOfWork.OrderRepository.GetHighestAmountOrder();

            var modelView =   new OrderViewModel()
            {
               orders =  orders.ToList(),
               HighestOrder = HighstOrder,       
            };
            return View(modelView);
          
          
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
              
               await _unitOfWork.OrderRepository.CreateOrder(order, createOrderViewModel.OrderItem);
               await _unitOfWork.SaveChangesAsync();
             
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrder(id);

                if (order != null)
                {
                    await _unitOfWork.OrderRepository.DeleteOrder(order);
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
