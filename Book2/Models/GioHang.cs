namespace Book2.Models
{
    public class GioHang
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();
    }
}