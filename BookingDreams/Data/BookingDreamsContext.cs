using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Data
{
    public class BookingDreamsContext:IdentityDbContext<TaiKhoan>
    {
        public BookingDreamsContext(DbContextOptions<BookingDreamsContext> opt):base(opt) 
        { 
            
        }
        #region
            public DbSet<KhachSan>? KhachSans { get; set; }
            public DbSet<TinhThanh>? TinhThanhs { get; set; }
            public DbSet<Phong>? Phongs { get; set; }
            public DbSet<DatPhong>? DatPhongs { get; set; }
            //public DbSet<DichVu>? DichVus { get; set; }
            public DbSet<KhachHang>? KhachHangs { get; set; }
            //public DbSet<ThanhToan>? ThanhToans { get; set; }
            //public DbSet<Role>? Roles { get; set; }
            //public DbSet<PhanQuyen>? PhanQuyens { get; set; }
            //public DbSet<ChucVu>? ChucVus { get; set; }
            //public DbSet<HinhAnh>? HinhAnhs { get; set; }
            public DbSet<DanhGia>? DanhGias { get; set; }
            public DbSet<MaGiamGia>? MaGiamGias { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { Name = "Admin", NormalizedName ="Admin", ConcurrencyStamp = "1"},
                    new IdentityRole() { Name = "NhanVien", NormalizedName ="NhanVien", ConcurrencyStamp = "2"},
                    new IdentityRole() { Name = "KhachHang", NormalizedName ="KhachHang", ConcurrencyStamp = "3"}
                );
        }
    }
}
