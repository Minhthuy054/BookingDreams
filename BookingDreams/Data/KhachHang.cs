using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public bool GioiTinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}
