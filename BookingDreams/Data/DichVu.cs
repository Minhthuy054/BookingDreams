using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        public int Id { get; set; }
        public string TenDichVu { get; set; }
        public int GiaTien { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
