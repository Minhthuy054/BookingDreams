using System.IdentityModel.Tokens.Jwt;

namespace BookingDreams.Models
{
    public class UserModel
    {
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string CCCD { get; set; }
        public bool GioiTinh { get; set; }
        public string Token { get; set; }
    }
}
