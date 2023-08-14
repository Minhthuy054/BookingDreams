using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("HinhAnh")]
    public class HinhAnh
    {
        [Key]
        public int Id { get; set; }
        public string Link { get; set; }
        public int ThuTuAnh { get; set; }
    }
}
