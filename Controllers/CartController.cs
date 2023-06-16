using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.Repository;

namespace Product_management.Controllers
{
    public class CartController : Controller
    {
        // GET: HomeController1
        private readonly IProductRepository _productRepository;
        private readonly ICartRepositorycs _cartRepository;

        public CartController (IProductRepository productRepository, ICartRepositorycs cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public ActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();
            var x = _productRepository.GetProductById(1);
            List<CartViewModel> cartViewModel = _cartRepository.GetAll().Select(x => new CartViewModel()
            {
                Id = x.Id,
                productName = _productRepository.GetProductById(x.ProductId).Name,
                    price1 = _productRepository.GetProductById(x.ProductId).Price,
                quantity = x.quantity,
                ProductId = x.ProductId,
               price = _productRepository.GetProductById(x.ProductId).Price * x.quantity,

            }).ToList();

            List<Cart> carts = _cartRepository.GetAll().ToList();
             //var product = _productRepository.GetProductById(carts[0].ProductId);
            return View(cartViewModel);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductId,quantity")] Cart cart)
        {
            try

            {
                var x = _cartRepository.GetAll().FirstOrDefault(item => item.ProductId == cart.ProductId);
                //var check = _cartRepository.GetAll().Where(x => x.ProductId == cart.ProductId).FirstOrDefault();
                if(x != null)
                {

                    var cartToUpdate = _cartRepository.GetAll().FirstOrDefault(x => x.ProductId == cart.ProductId);
                    cartToUpdate.quantity += 1 ;
                    _cartRepository.UpdateCart(cartToUpdate);
                }
                else
                {
                    _cartRepository.CreateCart(cart);
                }
                /* var checkCard = _cartRepository.GetAll().Where(x => x.Product.Id == cart.Id).FirstOrDefault();

                 if (checkCard == null)
                 {
                     cart.quantity = 1;
                     cart.
                 }
                /* else
                 {
                     cart.quantity += 1;
                     _cartRepository.UpdateCart(cart);
                 }
                 /* 
                 */
                return RedirectToAction("Index", "Product"); ;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Up(int id, IFormCollection collection)
        {
            try
            {
                var cartToUpdate = _cartRepository.GetCartById(id);
                cartToUpdate.quantity += 1;
                _cartRepository.UpdateCart(cartToUpdate);
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
                var cartToUpdate = _cartRepository.GetCartById(id);
                cartToUpdate.quantity -= 1;
                if(cartToUpdate.quantity == 0) {
                _cartRepository.DeleteCart(cartToUpdate);
                }
                else { _cartRepository.UpdateCart(cartToUpdate); }
              
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
