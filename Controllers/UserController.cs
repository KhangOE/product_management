using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;

namespace Product_management.Controllers
{
    public class UserController : Controller
    {
        // GET: HomeController1

       
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
            
        }
        public ActionResult Index()
        {
          
            var users = _unitOfWork.UserRepository.GetUsers().ToList();

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



            List<UserItemViewModel> userItemViewModels = users.Select(x => new UserItemViewModel
            {
                Name = x.Name,
                Id = x.Id,
                numberOrder = x.Orders.Count(),
            }).ToList();

            UserViewModel viewModel = new UserViewModel() { 
                 userItemViewModels = userItemViewModels,
                 HighestUser = HighestOrderUser
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            try
            {
                _unitOfWork.UserRepository.CreateUser(model);
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
