namespace BookingDreams.Models
{
    public class PhongImge
    {
        public int Id { get; set; }
        public int IdKhachSan { get; set; }
        public string TenPhong { get; set; }
        public string SoPhong { get; set; }
        public int GiaPhong { get; set; }
        public int Loai { get; set; }
        public string MoTa { get; set; }
        public int SoLuongNguoiLon { get; set; }
        public int SoLuongTreEm { get; set; }

        public List<IFormFile> HinhAnhFile { get; set; }
    }
}
