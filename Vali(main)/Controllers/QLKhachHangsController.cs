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
    public class QLKhachHangsController : Controller
    {
        private readonly QLBanVaLiContext _context;

        public QLKhachHangsController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: QLKhachHangs
        public async Task<IActionResult> Index()
        {
            var qLBanVaLiContext = _context.TKhachHangs.Include(t => t.UsernameNavigation);
            return View(await qLBanVaLiContext.ToListAsync());
        }

        // GET: QLKhachHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .Include(t => t.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.MaKhanhHang == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // GET: QLKhachHangs/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.TUsers, "Username", "Username");
            return View();
        }

        // POST: QLKhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhanhHang,Username,TenKhachHang,NgaySinh,SoDienThoai,DiaChi,LoaiKhachHang,AnhDaiDien,GhiChu")] TKhachHang tKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.TUsers, "Username", "Username", tKhachHang.Username);
            return View(tKhachHang);
        }

        // GET: QLKhachHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.TUsers, "Username", "Username", tKhachHang.Username);
            return View(tKhachHang);
        }

        // POST: QLKhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKhanhHang,Username,TenKhachHang,NgaySinh,SoDienThoai,DiaChi,LoaiKhachHang,AnhDaiDien,GhiChu")] TKhachHang tKhachHang)
        {
            if (id != tKhachHang.MaKhanhHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TKhachHangExists(tKhachHang.MaKhanhHang))
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
            ViewData["Username"] = new SelectList(_context.TUsers, "Username", "Username", tKhachHang.Username);
            return View(tKhachHang);
        }

        // GET: QLKhachHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .Include(t => t.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.MaKhanhHang == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // POST: QLKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang != null)
            {
                _context.TKhachHangs.Remove(tKhachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TKhachHangExists(string id)
        {
            return _context.TKhachHangs.Any(e => e.MaKhanhHang == id);
        }

    }
}
