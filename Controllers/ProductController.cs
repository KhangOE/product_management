using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;

namespace Product_management.Controllers
{
    public class ProductController : Controller
    {

       

    
        public IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {

         //   var products = _productRepository.GetAll();
          var products = _unitOfWork.ProductRepository.GetAll();
         // var product = _unitOfWork.p

            var curentTime = DateTime.Now;

            // create modelview wwith bough number
            var productItemViewModels = products
                .Select(x => new ProductItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    BoughNumber = x.OrderDetails.Where(od => od.Order.CreateDate.Month == curentTime.Month && od.Order.CreateDate.Year == curentTime.Year )
                   .Sum(x => x.quantity)
                }).ToList();


            //highest product bouogh this month
            var highestBoughProduct = products
                .OrderByDescending(p => p.OrderDetails
                    .Where(od => od.Order.CreateDate.Month == curentTime.Month && od.Order.CreateDate.Year == curentTime.Year)
                    .Sum(od => od.quantity))
                .FirstOrDefault();
             
            var productViewModel = new ProductViewModel
            {
               products = productItemViewModels,
               HighBoughProduct = highestBoughProduct,
            };
            return View(productViewModel);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      
        public IActionResult Create()
        { 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Description,Price")] Product product)
        {
            try
            {
              
               _unitOfWork.ProductRepository.CreateProduct(product);
               _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
               // return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            //  var product = _productRepository.GetProductById(id);

            var product = _unitOfWork.ProductRepository.GetProductById(id);
           // var product = null;
            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,Description,Price")] Product product)
        {
            try
            {
                if(_unitOfWork.ProductRepository.GetProductById(id) == null)
                {
                    return NotFound();
                }

                var productToUpdate = new Product
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };
                _unitOfWork.ProductRepository.UpdateProduct(productToUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
               var product = _unitOfWork.ProductRepository.GetProductById(id);

                if (product != null)
                {
                    _unitOfWork.ProductRepository.DeleteProduct(product);
                    _unitOfWork.Save();
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
