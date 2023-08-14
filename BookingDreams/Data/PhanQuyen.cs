using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        [Key] 
        public int Id { get; set;}
        public int IdQuyen { get; set; }
        public int IdTaiKhoan { get; set; }
    }
}
