using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;
using System.IO;
namespace Vali_main_.Controllers
{
    public class QLSanphamsController : Controller
    {
        private readonly QLBanVaLiContext _context;

        public QLSanphamsController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: QLSanphams
        public async Task<IActionResult> Index()
        {
            var qLBanVaLiContext = _context.TDanhMucSps.Include(t => t.MaChatLieuNavigation).Include(t => t.MaDtNavigation).Include(t => t.MaHangSxNavigation).Include(t => t.MaLoaiNavigation).Include(t => t.MaNuocSxNavigation);
            return View(await qLBanVaLiContext.ToListAsync());
        }

        // GET: QLSanphams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // GET: QLSanphams/Create
        public IActionResult Create()
        {
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "ChatLieu");
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "TenLoai");
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "HangSx");
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "Loai");
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "TenNuoc");
            return View();
        }

        // POST: QLSanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp, IFormFile ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Đặt tên ảnh đại diện mặc định nếu không có file được upload
                    tDanhMucSp.AnhDaiDien = "";

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string fileName = Path.GetFileName(ImageFile.FileName);
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                        // Lưu file vào thư mục
                        using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }
                        // Gán tên file vào thuộc tính AnhDaiDien của sản phẩm
                        tDanhMucSp.AnhDaiDien = fileName;
                    }
                    // Thêm sản phẩm vào context và lưu thay đổi
                    _context.Add(tDanhMucSp);
                    await _context.SaveChangesAsync();

                    // Điều hướng về trang Index sau khi tạo thành công
                    return RedirectToAction(nameof(Index));
                }

                return View(tDanhMucSp);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
                ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
                ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
                ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
                ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
                return View(tDanhMucSp);
            }
        }

        // GET: QLSanphams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);

            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "ChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "TenLoai", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "HangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "Loai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "TenNuoc", tDanhMucSp.MaNuocSx);

            return View(tDanhMucSp);
        }


        // POST: QLSanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IFormFile ImageFile, [Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (id != tDanhMucSp.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        tDanhMucSp.AnhDaiDien = fileName;
                    }
                    _context.Update(tDanhMucSp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ViewBag.Error = "Lỗi khi cập nhật dữ liệu!" + ex.Message;
                }
            }
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
            return View(tDanhMucSp);
        }

        // GET: QLSanphams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // POST: QLSanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp != null)
            {
                _context.TDanhMucSps.Remove(tDanhMucSp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDanhMucSpExists(string id)
        {
            return _context.TDanhMucSps.Any(e => e.MaSp == id);
        }
    }
}
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;
using System.IO;
namespace Vali_main_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QLSanphamsController : Controller
    {
        private readonly QLBanVaLiContext _context;

        public QLSanphamsController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: QLSanphams
        public async Task<IActionResult> Index()
        {
            var qLBanVaLiContext = _context.TDanhMucSps.Include(t => t.MaChatLieuNavigation).Include(t => t.MaDtNavigation).Include(t => t.MaHangSxNavigation).Include(t => t.MaLoaiNavigation).Include(t => t.MaNuocSxNavigation);
            return View(await qLBanVaLiContext.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDanhMucSp>>> GetSanphams()
        {
            var products = await _context.TDanhMucSps.ToListAsync();
            return Ok(products);
        }


        // GET: QLSanphams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // GET: QLSanphams/Create
        public IActionResult Create()
        {
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "ChatLieu");
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "TenLoai");
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "HangSx");
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "Loai");
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "TenNuoc");
            return View();
        }

        // POST: QLSanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp, IFormFile ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Đặt tên ảnh đại diện mặc định nếu không có file được upload
                    tDanhMucSp.AnhDaiDien = "";

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string fileName = Path.GetFileName(ImageFile.FileName);
                        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                        // Lưu file vào thư mục
                        using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }
                        // Gán tên file vào thuộc tính AnhDaiDien của sản phẩm
                        tDanhMucSp.AnhDaiDien = fileName;
                    }
                    // Thêm sản phẩm vào context và lưu thay đổi
                    _context.Add(tDanhMucSp);
                    await _context.SaveChangesAsync();

                    // Điều hướng về trang Index sau khi tạo thành công
                    return RedirectToAction(nameof(Index));
                }

                return View(tDanhMucSp);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
                ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
                ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
                ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
                ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
                return View(tDanhMucSp);
            }
        }

        // GET: QLSanphams/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);

            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "ChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "TenLoai", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "HangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "Loai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "TenNuoc", tDanhMucSp.MaNuocSx);

            return View(tDanhMucSp);
        }


        // POST: QLSanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IFormFile ImageFile, [Bind("MaSp,TenSp,MaChatLieu,NganLapTop,Model,CanNang,DoNoi,MaHangSx,MaNuocSx,MaDacTinh,Website,ThoiGianBaoHanh,GioiThieuSp,ChietKhau,MaLoai,MaDt,AnhDaiDien,GiaNhoNhat,GiaLonNhat")] TDanhMucSp tDanhMucSp)
        {
            if (id != tDanhMucSp.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        tDanhMucSp.AnhDaiDien = fileName;
                    }
                    _context.Update(tDanhMucSp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ViewBag.Error = "Lỗi khi cập nhật dữ liệu!" + ex.Message;
                }
            }
            ViewData["MaChatLieu"] = new SelectList(_context.TChatLieus, "MaChatLieu", "MaChatLieu", tDanhMucSp.MaChatLieu);
            ViewData["MaDt"] = new SelectList(_context.TLoaiDts, "MaDt", "MaDt", tDanhMucSp.MaDt);
            ViewData["MaHangSx"] = new SelectList(_context.THangSxes, "MaHangSx", "MaHangSx", tDanhMucSp.MaHangSx);
            ViewData["MaLoai"] = new SelectList(_context.TLoaiSps, "MaLoai", "MaLoai", tDanhMucSp.MaLoai);
            ViewData["MaNuocSx"] = new SelectList(_context.TQuocGia, "MaNuoc", "MaNuoc", tDanhMucSp.MaNuocSx);
            return View(tDanhMucSp);
        }

        // GET: QLSanphams/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tDanhMucSp = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            return View(tDanhMucSp);
        }

        // POST: QLSanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp != null)
            {
                _context.TDanhMucSps.Remove(tDanhMucSp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TDanhMucSpExists(string id)
        {
            return _context.TDanhMucSps.Any(e => e.MaSp == id);
        }
    }
}*/