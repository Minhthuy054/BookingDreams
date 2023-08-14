namespace BookingDreams.Models
{
    public class ThanhToanModel
    {
        public int Id { get; set; }
        public int IdDatPhong { get; set; }
        public int IdDichVu { get; set; }
        public int IdKhachHang { get; set; }
        public int TongThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string Ghichu { get; set; }
    }
}
