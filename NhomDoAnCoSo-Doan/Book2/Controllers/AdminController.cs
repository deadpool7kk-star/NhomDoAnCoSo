using Book2.Data;
using Book2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var model = new AdminDashboardVM
            {
                TongSach = await _context.Saches.CountAsync(),

                TongTheLoai = await _context.TheLoais.CountAsync(),

                TongDonHang = await _context.DonHangs.CountAsync(),

                TongDoanhThu = await _context.DonHangs
                    .Where(x => x.TrangThai == "Hoàn thành")
                    .SumAsync(x => (decimal?)x.TongTien) ?? 0,

                DonChoXacNhan = await _context.DonHangs
                    .CountAsync(x => x.TrangThai == "Chờ xác nhận"),

                DonDangGiao = await _context.DonHangs
                    .CountAsync(x => x.TrangThai == "Đang giao"),

                DonHoanThanh = await _context.DonHangs
                    .CountAsync(x => x.TrangThai == "Hoàn thành"),

                DonDaHuy = await _context.DonHangs
                    .CountAsync(x => x.TrangThai == "Đã hủy")
            };

            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "User");
            if (userRole != null)
            {
                model.TongKhachHang = await _context.UserRoles.CountAsync(x => x.RoleId == userRole.Id);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData(string period = "month")
        {
            var query = _context.DonHangs.Where(x => x.TrangThai == "Hoàn thành");
            var now = DateTime.Now;

            if (period == "day")
            {
                var startDate = now.AddDays(-29).Date;
                var rawData = await query
                    .Where(x => x.NgayDat >= startDate)
                    .ToListAsync();
                
                var data = rawData
                    .GroupBy(x => x.NgayDat.Date)
                    .Select(g => new { Date = g.Key, Total = g.Sum(x => x.TongTien) })
                    .ToList();

                var labels = new List<string>();
                var values = new List<decimal>();
                for (int i = 29; i >= 0; i--)
                {
                    var d = now.AddDays(-i).Date;
                    labels.Add(d.ToString("dd/MM"));
                    var item = data.FirstOrDefault(x => x.Date == d);
                    values.Add(item?.Total ?? 0);
                }
                return Json(new { labels, values });
            }
            else if (period == "month")
            {
                var rawData = await query
                    .Where(x => x.NgayDat.Year == now.Year)
                    .ToListAsync();
                
                var data = rawData
                    .GroupBy(x => x.NgayDat.Month)
                    .Select(g => new { Month = g.Key, Total = g.Sum(x => x.TongTien) })
                    .ToList();

                var labels = new List<string>();
                var values = new List<decimal>();
                for (int i = 1; i <= 12; i++)
                {
                    labels.Add($"Tháng {i}");
                    var item = data.FirstOrDefault(x => x.Month == i);
                    values.Add(item?.Total ?? 0);
                }
                return Json(new { labels, values });
            }
            else // year
            {
                var startYear = now.Year - 4;
                var rawData = await query
                    .Where(x => x.NgayDat.Year >= startYear)
                    .ToListAsync();

                var data = rawData
                    .GroupBy(x => x.NgayDat.Year)
                    .Select(g => new { Year = g.Key, Total = g.Sum(x => x.TongTien) })
                    .ToList();

                var labels = new List<string>();
                var values = new List<decimal>();
                for (int i = startYear; i <= now.Year; i++)
                {
                    labels.Add(i.ToString());
                    var item = data.FirstOrDefault(x => x.Year == i);
                    values.Add(item?.Total ?? 0);
                }
                return Json(new { labels, values });
            }
        }
    }
}