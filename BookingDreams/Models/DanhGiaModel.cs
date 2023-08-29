using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Models
{
    public class DanhGiaModel
    {
        
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public int IdPhong { get; set; }
        [Required]
        public int IdDatPhong { get; set; }
        [Required,Range(1,5)]
        public int SoSao { get; set; }
        [Required]
        public DateTime NgayDanhGia { get; set; }
        [Required]
        public string BinhLuan { get; set; }
    }
}
