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
            return View();
        }
    }
}
