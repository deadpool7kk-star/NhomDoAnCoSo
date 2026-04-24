using Book2.Data;
using Book2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book2.Controllers
{
    [Authorize(Roles = "Admin,Staff")]  
    public class AdminSachController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminSachController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string? tuKhoa)
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

            var ds = await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            ViewBag.TuKhoa = tuKhoa;
            return View(ds);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadTheLoaiAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sach model, IFormFile? hinhUpload)
        {
            await LoadTheLoaiAsync();

            if (!ModelState.IsValid)
                return View(model);

            if (hinhUpload != null && hinhUpload.Length > 0)
            {
                model.HinhAnh = await SaveImageAsync(hinhUpload);
            }

            _context.Saches.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm sách thành công.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sach = await _context.Saches.FindAsync(id);
            if (sach == null) return NotFound();

            await LoadTheLoaiAsync();
            return View(sach);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sach model, IFormFile? hinhUpload)
        {
            await LoadTheLoaiAsync();

            if (!ModelState.IsValid)
                return View(model);

            var sachDb = await _context.Saches.FindAsync(model.Id);
            if (sachDb == null) return NotFound();

            sachDb.TenSach = model.TenSach;
            sachDb.TacGia = model.TacGia;
            sachDb.Gia = model.Gia;
            sachDb.SoLuong = model.SoLuong;
            sachDb.MoTa = model.MoTa;
            sachDb.TheLoaiId = model.TheLoaiId;

            if (hinhUpload != null && hinhUpload.Length > 0)
            {
                sachDb.HinhAnh = await SaveImageAsync(hinhUpload);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cập nhật sách thành công.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sach = await _context.Saches.FindAsync(id);
            if (sach == null) return NotFound();

            bool daCoTrongDonHang = await _context.ChiTietDonHangs.AnyAsync(x => x.SachId == id);
            if (daCoTrongDonHang)
            {
                TempData["Error"] = "Không thể xóa sách vì sách đã tồn tại trong đơn hàng.";
                return RedirectToAction(nameof(Index));
            }

            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xóa sách thành công.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadTheLoaiAsync()
        {
            ViewBag.TheLoaiList = await _context.TheLoais
                .OrderBy(x => x.TenTheLoai)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.TenTheLoai
                })
                .ToListAsync();
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var folder = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/images/" + fileName;
        }
    }
}