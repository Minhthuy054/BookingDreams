using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Data
{
    public class TaiKhoan : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
    }
}
