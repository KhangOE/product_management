using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
namespace Product_management.Controllers
{
    public class ProductController : Controller
    {

        // private readonly IProductRepository = new _productRepository;
        //private DataContext db = new DataContext();

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext,IUserRepository   userRepository ,ILogger<ProductController> logger, IProductRepository productRepository,IOrderRepository orderRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
             _orderRepository = orderRepository;
                _userRepository = userRepository;
                _dataContext = dataContext;
                
        }





        public ActionResult Index()
        {

            var products = _productRepository.GetAll();

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

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Description,Price")] Product product)
        {
            try
            {
              //  var user = _userRepository.GetUserById(1);
              //  var order = new Order()
              //  {
             //      UserId = 1
              //  };
               // _orderRepository.CreateOrder(order,user,new List<int> { 1, 2, 3 });
                _productRepository.CreateProduct(product);
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
            var product = _productRepository.GetProductById(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Name,Description,Price")] Product product)
        {
            try
            {
                var productToUpdate = new Product
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };
                _productRepository.UpdateProduct(productToUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _productRepository.DeleteProduct(_productRepository.GetProductById(id));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
