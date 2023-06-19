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
        public ActionResult Index()
        {
           // var products = _productRepository.GetAll().ToList();
            //var x = _productRepository.GetProductById(1);
            List<CartViewModel> cartViewModel = _unitOfWork.cartRepositorycs
                .GetAll()
                .Select(x => new CartViewModel()
            {
                Id = x.Id,
                productName = _unitOfWork.ProductRepository.GetProductById(x.ProductId).Name,
                price1 = _unitOfWork.ProductRepository.GetProductById(x.ProductId).Price,
                quantity = x.quantity,
                ProductId = x.ProductId,
                price = _unitOfWork.ProductRepository.GetProductById(x.ProductId).Price * x.quantity,
            }).ToList();
            return View(cartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductId,quantity")] Cart cart)
        {
            try

            {
                var cartItem = _unitOfWork.cartRepositorycs
                    .GetAll()
                    .FirstOrDefault(item => item.ProductId == cart.ProductId);
                if(cartItem != null)
                {

                    var cartToUpdate = _unitOfWork.cartRepositorycs
                        .GetAll()
                        .FirstOrDefault(x => x.ProductId == cart.ProductId);
                    cartToUpdate.quantity += 1 ;
                    _unitOfWork.cartRepositorycs.UpdateCart(cartToUpdate);
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.cartRepositorycs.CreateCart(cart);
                    _unitOfWork.Save();
                }
                return RedirectToAction("Index", "Product"); ;
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Up(int id, IFormCollection collection)
        {
            try
            {
                var cartToUpdate = _unitOfWork.cartRepositorycs.GetCartById(id);
                cartToUpdate.quantity += 1;
                _unitOfWork.cartRepositorycs.UpdateCart(cartToUpdate);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult down(int id, IFormCollection collection)
        {
            try
            {
                var cartToUpdate = _unitOfWork.cartRepositorycs.GetCartById(id);
                cartToUpdate.quantity -= 1;
                if(cartToUpdate.quantity == 0) {
                    _unitOfWork.cartRepositorycs.DeleteCart(cartToUpdate);
                    _unitOfWork.Save();
                }
                else {
                    _unitOfWork.cartRepositorycs.UpdateCart(cartToUpdate);
                    _unitOfWork.Save();
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
