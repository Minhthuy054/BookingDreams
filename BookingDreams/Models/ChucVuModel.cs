using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Models
{
    public class ChucVuModel
    {
        [Key]
        public int Id { get; set; }
        public string TenChucVu { get; set; }
        public string GhiChu { get; set; }
    }
}
