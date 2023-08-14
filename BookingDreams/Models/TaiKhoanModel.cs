namespace BookingDreams.Models
{
    public class TaiKhoanModel
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int IdChucVu { get; set; }
    }
}
