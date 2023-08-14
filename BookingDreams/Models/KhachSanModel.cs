using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Models
{
    public class KhachSanModel
    {
        [Key]
        public int Id { get; set; }
        public int IdTinhThanh { get; set; }
        public string HinhAnh { get; set; }
        public string MaKhachSan { get; set; }
        public string TenKhachSan { get; set; }
        public string? DiaChi { get; set; }
        public string? GioiThieu { get; set; }
        public string? TieuDe { get; set; }
        public string? GhiChu { get; set; }
    }
}
