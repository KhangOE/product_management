using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;
using Product_management.ModelsTest;
namespace Product_management.Controllers
{
    public class ProductController : Controller
    {

        public IUnitOfWork _unitOfWork;
        public ISer1 _ser1;
        public IS2 _ser2;
        public ProductController(IUnitOfWork unitOfWork,ISer1 ser1, IS2 ser2)
        {
            _unitOfWork = unitOfWork;
            _ser1 = ser1;
            _ser2 = ser2;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            var curentTime = DateTime.Now;

            // create modelview wwith bough number
            var productItemViewModels = products
                .Select(x => new ProductItemViewModel
                {
                    testscoped = _ser2.getRandomNumber(),
                    test = _ser1.getRandomNumber(),
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    BoughNumber = x.OrderDetails.Where(od => od.Order.CreateDate.Month == curentTime.Month && od.Order.CreateDate.Year == curentTime.Year)
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
        public async Task<IActionResult> Create(Product product)
        {
            using var transaction = _unitOfWork.dbTransaction();
            try
            {
                await _unitOfWork.ProductRepository.CreateProduct(product);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {   
                transaction.Rollback();
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
