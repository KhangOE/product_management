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
        public async Task<IActionResult> Index()
        {
           
       
            var products = await _unitOfWork.ProductRepository.GetAll();

            var curentTime = DateTime.Now;

            // create modelview wwith bough number
            var productItemViewModels =  products
                .Select(x => new ProductItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    BoughNumber = x.OrderDetails.Where(od => od.Order.CreateDate.Month == curentTime.Month && od.Order.CreateDate.Year == curentTime.Year )
                   .Sum(x => x.quantity)
                }).ToList();


            //highest product bough this month
            Product HighBoughProduct =  await _unitOfWork.OrderRepository.HighestBoughProduct();
             
            var productViewModel = new ProductViewModel
            {
               products = productItemViewModels,
               HighBoughProduct = HighBoughProduct,
            };
            return View(productViewModel);
        }

        public IActionResult Create()
        { 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] Product product)
        {
            try
            {
              
               await _unitOfWork.ProductRepository.CreateProduct(product);
               await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
              
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _unitOfWork.ProductRepository.GetProductById(id);
            if (product != null)
            {
                return View(product);
            }

            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price")] Product product)
        {
            try
            {
             

                 await _unitOfWork.ProductRepository.UpdateProduct(new Product
                  {
                      Id = id,
                      Name = product.Name,
                      Description = product.Description,
                      Price = product.Price,
                  });
                  await _unitOfWork.SaveChangesAsync();


                return  RedirectToAction(nameof(Index));
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
               var product = await _unitOfWork.ProductRepository.GetProductById(id);

                if (product != null)
                {
                  
                     await _unitOfWork.ProductRepository.DeleteProduct(product);
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
