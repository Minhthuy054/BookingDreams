using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("TinhThanh")]
    public class TinhThanh
    {
        [Key]
        public int Id { get; set; }
        public string MaTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }
        public string? GhiChu { get; set; }
    }
}
