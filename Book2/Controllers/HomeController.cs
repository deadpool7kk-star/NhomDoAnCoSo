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

        public async Task<IActionResult> Index(string? tuKhoa, int? theLoaiId, string? sapXep)
        {
            var query = _context.Saches
                .Include(x => x.TheLoai)
                .Where(x => x.SoLuong > 0)
                .AsQueryable();

            bool isSearchMode = false;

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x =>
                    x.TenSach.Contains(tuKhoa) ||
                    x.TacGia.Contains(tuKhoa));
                isSearchMode = true;
            }

            if (theLoaiId.HasValue)
            {
                query = query.Where(x => x.TheLoaiId == theLoaiId.Value);
                isSearchMode = true;

                var theLoai = await _context.TheLoais.FindAsync(theLoaiId.Value);
                if (theLoai != null)
                {
                    ViewBag.TenTheLoai = theLoai.TenTheLoai;
                }
            }

            if (!string.IsNullOrEmpty(sapXep))
            {
                isSearchMode = true;
            }

            query = sapXep switch
            {
                "gia_tang" => query.OrderBy(x => x.Gia),
                "gia_giam" => query.OrderByDescending(x => x.Gia),
                "ten_az" => query.OrderBy(x => x.TenSach),
                _ => query.OrderByDescending(x => x.NgayTao)
            };

            var dsSach = await query.ToListAsync();

            ViewBag.TuKhoa = tuKhoa;
            ViewBag.TheLoaiId = theLoaiId;
            ViewBag.SapXep = sapXep;

            if (isSearchMode)
            {
                return View("Search", dsSach);
            }

            return View("LandingPage", dsSach);
        }
    }
}