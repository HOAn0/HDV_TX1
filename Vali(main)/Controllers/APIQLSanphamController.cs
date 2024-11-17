using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vali_main_.Models;
using System.IO;

namespace Vali_main_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIQLSanphamController : ControllerBase
    {
        private readonly QLBanVaLiContext _context;

        public APIQLSanphamController(QLBanVaLiContext context)
        {
            _context = context;
        }

        // GET: api/QLSanphams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDanhMucSp>>> GetTDanhMucSps()
        {
            var products = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/QLSanphams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TDanhMucSp>> GetTDanhMucSp(string id)
        {
            var product = await _context.TDanhMucSps
                .Include(t => t.MaChatLieuNavigation)
                .Include(t => t.MaDtNavigation)
                .Include(t => t.MaHangSxNavigation)
                .Include(t => t.MaLoaiNavigation)
                .Include(t => t.MaNuocSxNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/QLSanphams
        [HttpPost]
        public async Task<ActionResult<TDanhMucSp>> PostTDanhMucSp([FromForm] TDanhMucSp tDanhMucSp, [FromForm] IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                tDanhMucSp.AnhDaiDien = "";

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    tDanhMucSp.AnhDaiDien = fileName;
                }

                _context.TDanhMucSps.Add(tDanhMucSp);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTDanhMucSp), new { id = tDanhMucSp.MaSp }, tDanhMucSp);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error: {ex.Message}" });
            }
        }

        // PUT: api/QLSanphams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTDanhMucSp(string id, [FromForm] IFormFile ImageFile, [FromForm] TDanhMucSp tDanhMucSp)
        {
            if (id != tDanhMucSp.MaSp)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    tDanhMucSp.AnhDaiDien = fileName;
                }

                _context.Entry(tDanhMucSp).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TDanhMucSpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(new { message = $"Error: {ex.Message}" });
                }
            }
        }

        // DELETE: api/QLSanphams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTDanhMucSp(string id)
        {
            var tDanhMucSp = await _context.TDanhMucSps.FindAsync(id);
            if (tDanhMucSp == null)
            {
                return NotFound();
            }

            _context.TDanhMucSps.Remove(tDanhMucSp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TDanhMucSpExists(string id)
        {
            return _context.TDanhMucSps.Any(e => e.MaSp == id);
        }
    }
}
