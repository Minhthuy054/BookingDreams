using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Data
{
    public class TaiKhoan : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
    }
}
