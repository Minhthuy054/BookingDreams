using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("MaGiamGia")]
    public class MaGiamGia
    {
        [Key]
        public int Id { get; set; }
        public int IdKhachSan { get; set; }
        public string Ma { get; set; }
        public double GiaTri { get; set; }

    }
}
