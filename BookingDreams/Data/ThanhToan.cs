using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("ThanhToan")]
    public class ThanhToan
    {
        [Key] public int Id { get; set; }
        public int IdDatPhong { get; set; }
        public int IdDichVu { get; set; }
        public int IdKhachHang { get; set; }
        public double TongThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string Ghichu { get; set; }
    }
}
