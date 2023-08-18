namespace BookingDreams.Models
{
    public class PhongImge
    {
        public int Id { get; set; }
        public int IdKhachSan { get; set; }
        public string TenPhong { get; set; }
        public string SoPhong { get; set; }
        public string TrangThai { get; set; }
        public int GiaPhong { get; set; }
        public int IdLoai { get; set; }
        public string MoTa { get; set; }
        public int SoLuong { get; set; }
        public string Kieu { get; set; }
        public bool Active { get; set; }

        public List<IFormFile> HinhAnhFile { get; set; }
    }
}
