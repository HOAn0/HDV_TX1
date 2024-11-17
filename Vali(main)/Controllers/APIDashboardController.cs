using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vali_main_.Controllers
{
    public class APIDashboardController : Controller
    {
        readonly QLBanVaLiContext _context;
        public APIDashboardController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var monthlyRevenue = await _context.THoaDonBans
                                              .GroupBy(o => o.NgayHoaDon.GetValueOrDefault().Month)
                                              .Select(g => new MonthlyRevenueModel
                                              {
                                                  Month = g.Key,
                                                  Revenue = g.Sum(o => o.TongTienHd)
                                              })
                                              .ToListAsync();

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
                TotalProducts = totalProducts,
                MonthlyRevenue = monthlyRevenue
            };

            return View(dashboardViewModel);
        }



    }
}
