using System.ComponentModel.DataAnnotations;

namespace Book2.ViewModels
{
    public class ThanhToanVM
    {
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận")]
        public string TenNguoiNhan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        public string DiaChiNhanHang { get; set; } = string.Empty;

        public List<GioHangItemVM> GioHangItems { get; set; } = new();

        public decimal TongTien { get; set; }
    }
}