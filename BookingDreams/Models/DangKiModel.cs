using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Models
{
    public class DangKiModel
    {
        [Required]
        public string HoTen { get; set; } = null!;
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string CCCD { get; set; }
        [Required]
        public bool GioiTinh { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string DiaChi { get; set; }
    }
}
