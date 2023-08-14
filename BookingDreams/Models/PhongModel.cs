namespace BookingDreams.Models
{
    public class PhongModel
    {
        public int Id { get; set; }
        public int IdKhachSan { get; set; }
        public string TenPhong { get; set; }
        public string SoPhong { get; set; }
        public string TrangThai { get; set; }
        public int GiaPhong { get; set; }
        public int IdLoai { get; set; }
        public string HinhAnh { get; set; }
        public bool Active { get; set; }
    }
}
