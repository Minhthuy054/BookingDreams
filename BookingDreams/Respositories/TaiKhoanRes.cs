using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingDreams.Respositories
{
    public class TaiKhoanRes 
    { 
        private readonly UserManager<TaiKhoan> userManager;
        private readonly SignInManager<TaiKhoan> signInManager;
        private readonly IConfiguration configuration;
        private readonly BookingDreamsContext context;
        private readonly IMapper mapper;

        public TaiKhoanRes(UserManager<TaiKhoan> userManager, SignInManager<TaiKhoan> signInManager , IConfiguration configuration, BookingDreamsContext context, IMapper mapper) {
            this.userManager = userManager; 
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IdentityResult> DangKi(DangKiModel dk)
        {
            var user = new TaiKhoan
            {
                HoTen = dk.HoTen,
                NgaySinh = dk.NgaySinh,
                Email = dk.Email,
                UserName = dk.Email
            };
            var khachHang = new KhachHangModel
            {
                HoTen = dk.HoTen,
                CCCD = dk.CCCD,
                GioiTinh = dk.GioiTinh,
                SDT = dk.SDT,
                DiaChi = dk.DiaChi,
                NgaySinh = dk.NgaySinh
            };
            var newKhachhang = mapper.Map<KhachHang>(khachHang);
            context.KhachHangs!.Add(newKhachhang);
            await context.SaveChangesAsync();
            return await userManager.CreateAsync(user, dk.Password);
        }
        public async Task<string> DangNhap(DangNhapModel dn)
        {
            var result = signInManager.PasswordSignInAsync(dn.Email, dn.Password, false, false);
            if (!result.IsCompletedSuccessfully)
            {
                return string.Empty;
            }
            var authClaims =new List<Claim>
            {
                new Claim(ClaimTypes.Email, dn.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //new Claim(ClaimTypes.Email, dn.Email),
            };
            var authenKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires:DateTime.Now.AddMinutes(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey,SecurityAlgorithms.HmacSha512Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
