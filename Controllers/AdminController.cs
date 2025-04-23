using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SignIn([FromBody] SignInVM vm)
        {
            if (string.IsNullOrEmpty(vm.UserName))
            {
                return Json(new
                {
                    Success = false,
                    Message = "Please Enter UserName"
                });
            }
            else if (string.IsNullOrEmpty(vm.Password))
            {
                return Json(new
                {
                    Success = false,
                    Message = "Please Enter Password"
                });
            }
            else
            {
                User user = _context.User.Where(x => x.UserName == vm.UserName && x.Password == vm.Password).FirstOrDefault();
                if (user == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "User Not Found"
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = true,
                        Message = "Login Successful"
                    });
                }
            }

        }
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateSignUp([FromBody] User user)
        {
            var existingUserName = _context.User.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (existingUserName == null)
            {
                User u = new User();
                u.UserName = user.UserName;
                u.Password = user.Password;
                _context.User.Add(u);
                _context.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "User Created Successfully"
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Username Already Exists"
                });
            }
        }
       
    }
}
