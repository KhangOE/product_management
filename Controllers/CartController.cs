using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.Repository;
using Product_management.unitOfWork;

namespace Product_management.Controllers
{
    public class CartController : Controller
    {
        
       
        private readonly IUnitOfWork _unitOfWork;

        public CartController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
          
            var products = await _unitOfWork.ProductRepository.GetAll();
            var carts = await _unitOfWork.cartRepositorycs.GetAll();
            var f = new Product();
            f.Description = string.Empty;
            List<CartViewModel> cartViewModel = carts.ToList()
                .Select(x => new CartViewModel()
                {
                    Id = x.Id, 
                    productName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name,
                    price1 =      products.Where(y => y.Id == x.ProductId).FirstOrDefault().Price,
                    quantity = x.quantity,
                    ProductId = x.ProductId,
                    price = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Price  * x.quantity,
                }).ToList(); 


            return View(cartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,quantity")] Cart cart)
        {
            try

            {
                var carts = await _unitOfWork.cartRepositorycs.GetAll();
                var cartItem = carts.FirstOrDefault(x => x.ProductId == cart.ProductId);

                if(cartItem != null)
                {
                    var cartToUpdate = carts
                     .FirstOrDefault(x => x.ProductId == cart.ProductId);
                    cartToUpdate.quantity += 1 ;
                     await _unitOfWork.cartRepositorycs.UpdateCart(cartToUpdate);  
                }
                else
                {
                    await _unitOfWork.cartRepositorycs.CreateCart(cart);
                }
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index", "Product"); ;
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Up(int id)
        {
            try
            {
                var cartItem = await _unitOfWork.cartRepositorycs.GetCartById(id);
                if(cartItem != null)
                {
                    cartItem.quantity += 1;
                    await _unitOfWork.cartRepositorycs.UpdateCart(cartItem);
                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> down(int id, IFormCollection collection)
        {
            try
            {
                var cartToUpdate = await _unitOfWork.cartRepositorycs.GetCartById(id);
                cartToUpdate.quantity -= 1;
                if(cartToUpdate.quantity == 0) {
                    await _unitOfWork.cartRepositorycs.DeleteCart(cartToUpdate);
                   
                }
                else {
                    await _unitOfWork.cartRepositorycs.UpdateCart(cartToUpdate);
                   
                }
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
