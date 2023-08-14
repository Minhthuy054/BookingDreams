namespace BookingDreams.Models
{
    public class DatPhongModel
    {
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public int IdPhong { get; set; }
        public DateTime ThoiGianNhanPhong { get; set; }
        public DateTime ThoiGianTraPhong { get; set; }
        public int TongTien { get; set; }
        public string MaGiamGia { get; set; }
        public bool ThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
    }
}
