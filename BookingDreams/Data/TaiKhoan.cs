using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int IdChucVu { get; set; }
    }
}
