namespace BookingDreams.Models
{
    public class KhachSanImg
    {
        public int Id { get; set; }
        public int IdTinhThanh { get; set; }
        public string MaKhachSan { get; set; }
        public string TenKhachSan { get; set; }
        public string? DiaChi { get; set; }
        public string? GioiThieu { get; set; }
        public string? TieuDe { get; set; }
        public string? GhiChu { get; set; }


        public List<IFormFile> HinhAnhFile { get; set; }
    }
}
