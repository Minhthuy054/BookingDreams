using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingDreams.Data
{
    [Table("Role")]
    public class Role
    {
        [Key] public int Id { get; set; }
        public string TenQuyen { get; set; }
        public string GhiChu { get; set; }
    }
}
