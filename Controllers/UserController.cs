using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
namespace Product_management.Controllers
{
    public class UserController : Controller
    {
        // GET: HomeController1

        private readonly IUserRepository _userRepository;
        private readonly DataContext _dataContext;
        public UserController(IUserRepository userRepository, DataContext datacontext)
        {
            _userRepository = userRepository;
            _dataContext = datacontext;
            
        }
        public ActionResult Index()
        {
          
            var users = _userRepository.GetUsers().ToList();

            var currentTime = DateTime.Now;

            // user has highest number order this month
            var HighestOrderUser = users
                .FirstOrDefault(u => u.Orders
                    .Where(o => o.CreateDate.Month == currentTime.Month && o.CreateDate.Year == currentTime.Year)
                    .Count()
                    == users
                   .Max(x => x.Orders
                        .Where(o => o.CreateDate.Month == currentTime.Month && o.CreateDate.Year == currentTime.Year)
                        .Count()));



            List<UserItemViewModel> userIteViewModels = users.Select(x => new UserItemViewModel
            {
                Name = x.Name,
                Id = x.Id,
                numberOrder = x.Orders.Count(),
            }).ToList();

            UserViewModel viewModel = new UserViewModel() { 
            userItemViewModels = userIteViewModels,
            HighestUser = HighestOrderUser
            };

            return View(viewModel);
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
        public ActionResult Create([Bind("Name,Email,Password")] User model)
        {
            try
            {

               /* var user = new User
                {
                    Email = "12",
                    Password = "23123",
                    Name = "12313",
                };*/
               // _dataContext.Add(model);
               // _dataContext.SaveChanges();


                  _userRepository.CreateUser(model);

                //_dataContext.SaveChanges();
                return RedirectToAction(nameof(Index));
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
