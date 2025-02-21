using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllData()
        {
            var data = _context.Category.ToList();
            return Json(new
            {
                success = true,
                data = data
            });
        }
        public JsonResult Save(string categoryName,string categoryCode,int id)
        {
            if(id == 0)
            {
                Category c = new Category()
                {
                    CategoryName = categoryName,
                    CategoryUnit = categoryCode,
                    CreatedDate = DateTime.Now,
                };
                _context.Category.Add(c);
                _context.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Data Saved Successfully"
                });
            }
            else
            {
                var oldData = _context.Category.Where(x => x.Id == id).FirstOrDefault();
                if (oldData == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Data Not Found"
                    });
                }
                else
                {
                    oldData.CategoryName = categoryName;
                    oldData.CategoryUnit = categoryCode;
                    oldData.CreatedDate = DateTime.Now;
                    _context.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "Succesfully Updated"
                    });
                }
            }
           
        }
        public JsonResult Delete(int id)
        {
           var data = _context.Category.Where(x => x.Id == id).FirstOrDefault();
            if(data == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Data Not Found"
                });
            }
            else
            {
               _context.Category.Remove(data);
               _context.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Data Deleted Successfully"
                });
            }
        }

        public JsonResult Edit (int id)
        {
            var data = _context.Category.Where(x => x.Id==id).FirstOrDefault();
            if(data == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Data Not Found"
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    data = data
                });
            }
        }

        public IActionResult Category(string categoryName, int id)
        {
            ItemListVM itemListVM = new ItemListVM();
            itemListVM.ProductItems = _context.ProductItems.Where(c => c.CategoryId == id).ToList();
            return View(itemListVM);
        }

    }
}
