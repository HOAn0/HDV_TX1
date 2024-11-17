using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;

namespace Vali_main_.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class APIKhachHangController : ControllerBase
    {
        private readonly QLBanVaLiContext _context; // Sử dụng đúng DbContext

        public APIKhachHangController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: api/khachhang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TKhachHang>>> GetKhachHangs()
        {
            return await _context.TKhachHangs.ToListAsync();
        }

        // GET: api/khachhang/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TKhachHang>> GetKhachHang(string id)
        {
            var khachHang = await _context.TKhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // POST: api/khachhang
        [HttpPost]
        public async Task<ActionResult<TKhachHang>> PostKhachHang(TKhachHang khachHang)
        {
            _context.TKhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKhanhHang }, khachHang);
        }

        // PUT: api/khachhang/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(string id, TKhachHang khachHang)
        {
            if (id != khachHang.MaKhanhHang)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/khachhang/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(string id)
        {
            var khachHang = await _context.TKhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.TKhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(string id)
        {
            return _context.TKhachHangs.Any(e => e.MaKhanhHang == id);
        }
    }
}
