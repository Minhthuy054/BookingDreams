using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public TaiKhoanRes(UserManager<TaiKhoan> userManager, SignInManager<TaiKhoan> signInManager,RoleManager<IdentityRole> roleManager , IConfiguration configuration, BookingDreamsContext context, IMapper mapper) {
            this.userManager = userManager; 
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }
        public async Task<IdentityResult> DangKi(DangKiModel dk,string role)
        {
            
            var userExist = await userManager.FindByEmailAsync(dk.Email);
            if(userExist != null)
            {
                var errors = new List<IdentityError>
                {
                    new IdentityError{Code ="Đăng kí không thàng công", Description = "Email đã tồn tại"  }
                };
                return IdentityResult.Failed(errors.ToArray());
            }
            //if (string.IsNullOrEmpty(role))
            //{
            //    var khachHang = new KhachHangModel
            //    {
            //        HoTen = dk.HoTen,
            //        CCCD = dk.CCCD,
            //        GioiTinh = dk.GioiTinh,
            //        SDT = dk.SDT,
            //        DiaChi = dk.DiaChi,
            //        NgaySinh = dk.NgaySinh
            //    };
            //    var newKhachhang = mapper.Map<KhachHang>(khachHang);
            //    context.KhachHangs!.Add(newKhachhang);
            //    await context.SaveChangesAsync();
            //}
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                var errors = new List<IdentityError>
                {
                    new IdentityError{Code ="Đăng kí không thàng công", Description = "Role không tồn tại"  }
                };
                return IdentityResult.Failed(errors.ToArray());
            }
            var user = new TaiKhoan
            {
                HoTen = dk.HoTen,
                NgaySinh = dk.NgaySinh,
                CCCD = dk.CCCD,
                GioiTinh = dk.GioiTinh,
                PhoneNumber = dk.SDT,
                DiaChi = dk.DiaChi,
                Email = dk.Email,
                UserName = dk.Email
            };
            
            var result = await userManager.CreateAsync(user, dk.Password);
            
            return await userManager.AddToRoleAsync(user, role);
        }
        public async Task<string> DangNhap(DangNhapModel dn)
        {
            var result = await signInManager.PasswordSignInAsync(dn.Email, dn.Password, false, false);
            if (!result.Succeeded)
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
