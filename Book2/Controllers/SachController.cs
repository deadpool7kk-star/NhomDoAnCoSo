using Book2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book2.Controllers
{
    public class SachController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SachController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var sach = await _context.Saches
                .Include(x => x.TheLoai)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sach == null)
            {
                return NotFound();
            }

            var sachLienQuan = await _context.Saches
                .Include(x => x.TheLoai)
                .Where(x => x.TheLoaiId == sach.TheLoaiId && x.Id != sach.Id)
                .OrderByDescending(x => x.Id)
                .Take(4)
                .ToListAsync();

            ViewBag.SachLienQuan = sachLienQuan;

            return View(sach);
        }
    }
}