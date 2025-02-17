using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ProductItemController : Controller
    {
        ApplicationDbContext _context;
        public ProductItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var datas = _context.ProductItems.ToList();
            return View(datas);
        }
        public JsonResult Save(string name,string code,int categoryId,string description,int unitPrice,string thumbnail)
        {
            ProductItem obj = new ProductItem()
            {
                ProductName = name,
                ProductCode = code,
                CategoryId = categoryId,
                Description = description,
                UnitPrice = unitPrice,
                Thumbnail = thumbnail
            };
            _context.ProductItems.Add(obj);
            _context.SaveChanges();
            return Json(new
            {
                success = true,
                message = "Product Item Saved Successfully"
            });
        }

        public JsonResult Delete(int id)
        {
            var data = _context.ProductItems.Where(x => x.ProductItemId == id).FirstOrDefault();
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
                _context.ProductItems.Remove(data);
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
