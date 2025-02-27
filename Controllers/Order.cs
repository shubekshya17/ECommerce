using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class Order : Controller
    {
        ApplicationDbContext _context;
        public Order(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult Save([FromBody] OrderVM orderVM)
        {
            if(string.IsNullOrEmpty(orderVM.master.FullName))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter FullName"
                });
            }
            else if (string.IsNullOrEmpty(orderVM.master.Email))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Email"
                });
            }
            else if (string.IsNullOrEmpty(orderVM.master.MobileNo))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Mobile No"
                });
            }
            else if (string.IsNullOrEmpty(orderVM.master.Address))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Address"
                });
            }
            else if(orderVM.detail.Count < 0)
            {
                return Json(new
                {
                    success = false,
                    message = "No Items Present In The Cart"
                });
            }
            else
            {
                ProductOrderMaster m = new ProductOrderMaster();
                m.FullName = orderVM.master.FullName;
                m.Email = orderVM.master.Email;
                m.Address = orderVM.master.Address;
                m.MobileNo = orderVM.master.MobileNo;
                m.OrderDate = DateTime.Now;
                m.GrandTotal = orderVM.detail.Sum(s => s.UnitPrice * s.Quantity);
                _context.ProductOrderMaster.Add(m);
                _context.SaveChanges();

                List<ProductOrderDetail> d = new List<ProductOrderDetail>();
                foreach(var item in orderVM.detail)
                {
                    d.Add(new ProductOrderDetail
                    {
                        ProductItemId = item.ProductItemId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        ProductOrderMasterId = m.ProductOrderMasterId
                    });
                }
                _context.ProductOrderDetail.AddRange(d);
                _context.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Order Placed Successfully"
                });
            }
        }
        public IActionResult Index()
        {
            /*USING JOIN
            var datas = _context.ProductOrderMaster
                .GroupJoin(_context.ProductOrderDetail,
                master => master.ProductOrderMasterId,
                detail => detail.ProductOrderMasterId,
                (master, detail) => new OrderWithCountVM
                {
                    ProductOrderMasterId = master.ProductOrderMasterId,
                    FullName = master.FullName,
                    Address = master.Address,
                    MobileNo = master.MobileNo,
                    TotalItems = detail.Count()
                }).ToList();*/
           /* USING SELECT*/
            var datas = _context.ProductOrderMaster
                 .Select(s => new OrderWithCountVM
                 {
                    ProductOrderMasterId = s.ProductOrderMasterId,
                    FullName = s.FullName,
                    Address = s.Address,
                    MobileNo = s.MobileNo,
                    GrandTotal = s.GrandTotal,
                    TotalItems = _context.ProductOrderDetail.Where(x => x.ProductOrderMasterId == s.ProductOrderMasterId).Count(),
                 });
            return View(datas);
        }
        public JsonResult ViewItems(int Id)
        {
            var datas = _context.ProductOrderDetail
                .Where(x => x.ProductOrderMasterId == Id)
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
            return Json(new
            {
                success = true,
                data = datas
            });
        }
    }
}
