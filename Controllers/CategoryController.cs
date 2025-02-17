using ECommerce.DataAccess;
using ECommerce.Models;
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
            /*Category c = new Category();
            c.CategoryName = "Shoes";
            c.CategoryUnit = "Unit";
            c.CreatedDate = DateTime.Now;
            _context.Add(c);
            _context.SaveChanges();*/

            var datas= _context.Category.ToList();
            return View(datas);
        }
        public JsonResult Save(string categoryName,string categoryCode)
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
    }
}
