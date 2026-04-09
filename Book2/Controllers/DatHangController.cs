using Book2.Data;
using Book2.Models;
using Book2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book2.Controllers
{
    [Authorize]
    public class DatHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private async Task<GioHang?> GetGioHangAsync()
        {
            var userId = GetUserId();

            return await _context.GioHangs
                .Include(x => x.ChiTietGioHangs)!
                .ThenInclude(x => x.Sach)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        // Màn hình thanh toán
        [HttpGet]
        public async Task<IActionResult> ThanhToan()
        {
            var gioHang = await GetGioHangAsync();

            if (gioHang == null || gioHang.ChiTietGioHangs == null || !gioHang.ChiTietGioHangs.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống.";
                return RedirectToAction("Index", "GioHang");
            }

            var model = new ThanhToanVM
            {
                GioHangItems = gioHang.ChiTietGioHangs
                    .Where(x => x.Sach != null)
                    .Select(x => new GioHangItemVM
                    {
                        ChiTietGioHangId = x.Id,
                        SachId = x.SachId,
                        TenSach = x.Sach!.TenSach,
                        TacGia = x.Sach.TacGia,
                        Gia = x.Sach.Gia,
                        SoLuong = x.SoLuong,
                        HinhAnh = x.Sach.HinhAnh
                    })
                    .ToList()
            };

            model.TongTien = model.GioHangItems.Sum(x => x.ThanhTien);

            return View(model);
        }

        // Xử lý đặt hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThanhToan(ThanhToanVM model)
        {
            var gioHang = await GetGioHangAsync();

            if (gioHang == null || gioHang.ChiTietGioHangs == null || !gioHang.ChiTietGioHangs.Any())
            {
                TempData["Error"] = "Giỏ hàng đang trống.";
                return RedirectToAction("Index", "GioHang");
            }

            var gioHangItems = gioHang.ChiTietGioHangs
                .Where(x => x.Sach != null)
                .Select(x => new GioHangItemVM
                {
                    ChiTietGioHangId = x.Id,
                    SachId = x.SachId,
                    TenSach = x.Sach!.TenSach,
                    TacGia = x.Sach.TacGia,
                    Gia = x.Sach.Gia,
                    SoLuong = x.SoLuong,
                    HinhAnh = x.Sach.HinhAnh
                })
                .ToList();

            model.GioHangItems = gioHangItems;
            model.TongTien = gioHangItems.Sum(x => x.ThanhTien);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // kiểm tra tồn kho trước khi tạo đơn
            foreach (var item in gioHang.ChiTietGioHangs)
            {
                if (item.Sach == null)
                {
                    TempData["Error"] = "Có sản phẩm không tồn tại trong giỏ hàng.";
                    return RedirectToAction("Index", "GioHang");
                }

                if (item.SoLuong > item.Sach.SoLuong)
                {
                    TempData["Error"] = $"Sách '{item.Sach.TenSach}' không đủ số lượng tồn kho.";
                    return RedirectToAction("Index", "GioHang");
                }
            }

            var donHang = new DonHang
            {
                UserId = GetUserId(),
                TenNguoiNhan = model.TenNguoiNhan,
                SoDienThoai = model.SoDienThoai,
                DiaChiNhanHang = model.DiaChiNhanHang,
                NgayDat = DateTime.Now,
                TongTien = model.TongTien,
                TrangThai = "Chờ xác nhận"
            };

            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            foreach (var item in gioHang.ChiTietGioHangs)
            {
                var sach = item.Sach!;

                _context.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    DonHangId = donHang.Id,
                    SachId = item.SachId,
                    SoLuong = item.SoLuong,
                    DonGia = sach.Gia
                });

                sach.SoLuong -= item.SoLuong;
            }

            _context.ChiTietGioHangs.RemoveRange(gioHang.ChiTietGioHangs);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đặt hàng thành công.";
            return RedirectToAction("DatHangThanhCong");
        }

        public IActionResult DatHangThanhCong()
        {
            return View();
        }

        // Lịch sử đơn hàng của user
        public async Task<IActionResult> LichSu()
        {
            var userId = GetUserId();

            var dsDonHang = await _context.DonHangs
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.NgayDat)
                .ToListAsync();

            return View(dsDonHang);
        }

        // Chi tiết 1 đơn hàng
        public async Task<IActionResult> ChiTiet(int id)
        {
            var userId = GetUserId();

            var donHang = await _context.DonHangs
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (donHang == null)
            {
                return NotFound();
            }

            var chiTiet = await _context.ChiTietDonHangs
                .Include(x => x.Sach)
                .Where(x => x.DonHangId == id)
                .ToListAsync();

            ViewBag.DonHang = donHang;
            return View(chiTiet);
        }
    }
}