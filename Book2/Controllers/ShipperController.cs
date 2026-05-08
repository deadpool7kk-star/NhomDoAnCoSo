using Microsoft.AspNetCore.Identity;
using Book2.Models;
using Book2.Data;
using Book2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book2.Controllers
{
    [Authorize(Roles = "Shipper")]
    public class ShipperController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShipperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);

            var model = new AdminDashboardVM
            {
                TongDonHang = await _context.DonHangs.CountAsync(x => x.ShipperId == userId),
                DonChoXacNhan = await _context.DonHangs.CountAsync(x => x.ShipperId == userId && x.TrangThai == "Chờ xác nhận"),
                DonDangGiao = await _context.DonHangs.CountAsync(x => x.ShipperId == userId && x.TrangThai == "Đang giao"),
                DonHoanThanh = await _context.DonHangs.CountAsync(x => x.ShipperId == userId && x.TrangThai == "Hoàn thành"),
                DonDaHuy = await _context.DonHangs.CountAsync(x => x.ShipperId == userId && x.TrangThai == "Đã hủy")
            };

            return View(model);
        }
    }
}