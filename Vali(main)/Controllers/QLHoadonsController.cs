using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;

namespace Vali_main_.Controllers
{
    public class QLHoadonsController : Controller
    {
        private readonly QLBanVaLiContext _context;

        public QLHoadonsController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: QLHoadons
        public async Task<IActionResult> Index()
        {
            var qLBanVaLiContext = _context.THoaDonBans.Include(t => t.MaKhachHangNavigation).Include(t => t.MaNhanVienNavigation);
            return View(await qLBanVaLiContext.ToListAsync());
        }

        // GET: QLHoadons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhachHangNavigation)
                .Include(t => t.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // GET: QLHoadons/Create
        public IActionResult Create()
        {
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhanhHang", "TenKhacHang");
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "TenNhanVien");
            return View();
        }

        // POST: QLHoadons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,MaSoThue,ThongTinThue,GhiChu")] THoaDonBan tHoaDonBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tHoaDonBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhanhHang", "MaKhanhHang", tHoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // GET: QLHoadons/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhanhHang", "MaKhanhHang", tHoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // POST: QLHoadons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,MaSoThue,ThongTinThue,GhiChu")] THoaDonBan tHoaDonBan)
        {
            if (id != tHoaDonBan.MaHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tHoaDonBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoaDonBanExists(tHoaDonBan.MaHoaDon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhanhHang", "MaKhanhHang", tHoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // GET: QLHoadons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhachHangNavigation)
                .Include(t => t.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // POST: QLHoadons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan != null)
            {
                _context.THoaDonBans.Remove(tHoaDonBan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool THoaDonBanExists(string id)
        {
            return _context.THoaDonBans.Any(e => e.MaHoaDon == id);
        }


    }
}
