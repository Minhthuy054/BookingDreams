using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Models
{
    public class DatPhongModel
    {
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

        public int IdPhong { get; set; }
        public DateTime ThoiGianNhanPhong { get; set; }
        public DateTime ThoiGianTraPhong { get; set; }
        public string MaGiamGia { get; set; }
        public double TongTien { get; set; }
        public float Label { get; set; }
    }
}
