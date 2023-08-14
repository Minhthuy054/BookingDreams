using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("ChucVu")]
    public class ChucVu
    {
        [Key]
        public int Id { get; set; }
        public string TenChucVu { get; set; }
        public string GhiChu { get; set; }
    }
}
