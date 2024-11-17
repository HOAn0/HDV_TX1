using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vali_main_.Models;

namespace Vali_main_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QLBanVaLiContext _context;
        public HomeController(ILogger<HomeController> logger, QLBanVaLiContext context)
        {
            _logger = logger;
            _context = context;
        }

        /*public IActionResult Index()
        {
            
            // Lấy tổng số khách hàng
            int totalCustomers = _context.TKhachHangs.Count();

            // Truyền dữ liệu qua ViewBag
            ViewBag.TotalCustomers = totalCustomers;
            return View();
        }*/

        public async Task<IActionResult> Index()
        {
            // Lấy thông tin tổng quan từ cơ sở dữ liệu
            var totalCustomers = await _context.TKhachHangs.CountAsync();
            var totalOrders = await _context.THoaDonBans.CountAsync();
            var totalRevenue = await _context.THoaDonBans.SumAsync(o => o.TongTienHd);  // Giả sử có trường Tổng Tiền trong hóa đơn
            var totalProducts = await _context.TDanhMucSps.CountAsync();

            // Truyền dữ liệu vào view
            var dashboardViewModel = new APIDashboardViewModel
            {
                TotalCustomers = totalCustomers,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                TotalProducts = totalProducts
            };

            return View(dashboardViewModel);
        }


        // API lấy dữ liệu doanh thu(Tổng tiền theo Month)
        [HttpGet]
        public JsonResult GetRevenueData()
        {
            var monthlyRevenue = _context.THoaDonBans
                                             .GroupBy(o => o.NgayHoaDon.GetValueOrDefault().Month)
                                             .Select(g => new MonthlyRevenueModel
                                             {
                                                 Month = g.Key,
                                                 Revenue = g.Sum(o => o.TongTienHd)
                                             })
                                             .ToListAsync();
            

            return Json(monthlyRevenue);
        }

        // API lấy dữ liệu số đơn hàng (số lượng đơn hàng theo ngày)
        [HttpGet]
        public JsonResult GetOrderData()
        {
            var orderData = _context.THoaDonBans
                .Where(t => t.NgayHoaDon.HasValue) // Kiểm tra ngày hóa đơn không null
                .GroupBy(t => t.NgayHoaDon.Value.Date) // Nhóm theo ngày
                .Select(g => new
                {
                    Date = g.Key,
                    TotalOrders = g.Count()  // Số đơn hàng theo ngày
                }).ToList();

            return Json(orderData);
        }
    }
}
