using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
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
        public JsonResult Save(string name, string code, int categoryId, string description, int unitPrice, string thumbnail, int id)
        {
            if (id == 0)
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
            else
            {
                var oldData = _context.ProductItems.Where(x => x.ProductItemId == id).FirstOrDefault();
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
                    oldData.ProductName = name;
                    oldData.ProductCode = code;
                    oldData.Thumbnail = thumbnail;
                    oldData.Description = description;
                    oldData.UnitPrice = unitPrice;
                    oldData.CategoryId = categoryId;
                    _context.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "Data Updated Successfully"
                    });
                }
            }

        }

        public JsonResult Delete(int id)
        {
            var data = _context.ProductItems.Where(x => x.ProductItemId == id).FirstOrDefault();
            if (data == null)
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

        public JsonResult Edit(int id)
        {
            var data = _context.ProductItems.Where(x => x.ProductItemId == id).FirstOrDefault();
            if (data == null)
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

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            var data = _context.ProductItems.Where(x => x.ProductItemId == id).FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return View(data);
            }
        }
        public IActionResult Pay(int id)
        {
            PayVM obj = new PayVM();
            var data = _context.ProductOrderMaster.Where(x => x.ProductOrderMasterId == id).FirstOrDefault();
            if (data != null)
            {
                obj.master = data;
            }
            obj.detail = _context.ProductOrderDetail
                .Where(x => x.ProductOrderMasterId == id)
                .Join(_context.ProductItems,
                master => master.ProductItemId,
                detail => detail.ProductItemId,
                (master, detail) => new OrderWithNameVM
                {
                    Quantity = master.Quantity,
                    UnitPrice = master.UnitPrice,
                    ProductName = detail.ProductName,
                    TotalPrice = master.Quantity * master.UnitPrice
                })
                .ToList();
            return View(obj);
        }
    }
}
