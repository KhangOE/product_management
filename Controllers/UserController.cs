using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Interface.Service;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;

namespace Product_management.Controllers
{
    public class UserController : Controller
    {
        // GET: HomeController1

       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public UserController(IUnitOfWork unitOfWork,IUserService userService)
        {
           
            _unitOfWork = unitOfWork;
            _userService = userService;
            
        }
        public async Task<IActionResult> Index()
        {
            //var x = new User();
            //var t = x.PeopleDictionary;
            var ut = await _unitOfWork.UserRepository.GetUserById(2);
            //Console.WriteLine("so luong" + ut.Adress["2"]);
       //     ut.Adress =new Dictionary<string, string> { { "0","1"},{"r","r" } };
            _unitOfWork.UserRepository.UpdateUser(ut);
            await _unitOfWork.SaveChangesAsync();
            var users = await _unitOfWork.UserRepository.GetUsers();

            var HighestOrderUser = await  _unitOfWork.OrderRepository.HighestOrderedUser();

            List<UserItemViewModel> userItemViewModels = users.Select(x => new UserItemViewModel
            {
                Name = x.Name,
                Id = x.Id,
                numberOrder = x.Orders.Count(),
            }).ToList();

            UserViewModel viewModel = new UserViewModel() { 
                 userItemViewModels = userItemViewModels,
                 HighestUser =  HighestOrderUser
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Like(int ProductId,int UserId)
        {
            try
            {/*
                var user =await _unitOfWork.UserRepository.GetUserById(ProductId);
                if (user.Productsliked.ContainsKey(ProductId))
                {
                    user.Productsliked.Remove(ProductId);
                }
                else
                {
                    user.Productsliked.Add(ProductId,await _unitOfWork.ProductRepository.GetProductById(ProductId));
                }*/
                return View();
            }
            catch
            {
                return View();

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            try
            {

                var user = new User { Email = model.Email,
                    Name = model.Name,
                    Password = model.Password,
                    Adress = new Dictionary<int, Address> {
                    { 1, new Address(){
                        Id =1,
                                        Name = "1",
                                        code = "3"
                                      }},
                    {2, new Address() {Id =2,Name ="3",code = "43" } } },
               // Productsliked = new Dictionary<int, Product> { { 1,new Product { Name = "name" ,Price = 123123,Description = "123123"} } }
             //   test = new Dictionary<int, int> { { 1, 2 }, { 3,4 } }
                };
                _unitOfWork.UserRepository.CreateUser(user);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public IActionResult Edit(int id)
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
        public async Task<IActionResult> SavingProduct(int UserId,int ProductId, IFormCollection collection)
        {
            try
            {
               

                await _userService.SavingProduct(UserId, ProductId);
                //   Console.WriteLine("user" + UserId+"+"+ProductId);
                return RedirectToAction("Index", "Product"); ;

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
