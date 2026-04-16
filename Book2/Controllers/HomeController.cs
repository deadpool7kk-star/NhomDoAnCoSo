using Book2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? tuKhoa, int? theLoaiId)
        {
            var query = _context.Saches
                .Include(x => x.TheLoai)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x =>
                    x.TenSach.Contains(tuKhoa) ||
                    x.TacGia.Contains(tuKhoa));
            }

            if (theLoaiId.HasValue)
            {
                query = query.Where(x => x.TheLoaiId == theLoaiId.Value);
            }

            var dsSach = await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            ViewBag.TheLoais = await _context.TheLoais.ToListAsync();
            ViewBag.TuKhoa = tuKhoa;
            ViewBag.TheLoaiId = theLoaiId;

            return View("LandingPage", dsSach);
        }
    }
}