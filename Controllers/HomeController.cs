using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            DashboardVM obj = new DashboardVM();
            obj.CategoryInfo = _context.Category.ToList();
            obj.ProductItemInfo = _context.ProductItems.ToList();
            return View(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetChartData()
        {
            var result = _context.ProductOrderDetail.GroupBy(x => x.ProductItemId,
                (key, g) => new
                {
                    ProductItemId = key,
                    TotalCount = g.Count(),
                });
            var data = _context.ProductItems
                .Select(s => new
                {
                    ProductItemId = s.ProductItemId,
                    ProductItemName = s.ProductName,
                })
                .ToList();
            return Json(new
            {
                Success = true,
                CountInfo = result,
                nameInfo = data
            });
        }
    }
}
