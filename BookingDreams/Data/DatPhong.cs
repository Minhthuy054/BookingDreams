using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;

namespace BookingDreams.Data
{
    [Table("DatPhong")]
    public class DatPhong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string HoTen { get; set; } = null!;
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string CCCD { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public int IdPhong { get; set; }
        [Required]
        public DateTime ThoiGianNhanPhong { get; set; }
        [Required]
        public DateTime ThoiGianTraPhong { get; set; }
        public string? MaGiamGia { get; set; } = null!;
        public double TongTien { get; set; }
    }
}
