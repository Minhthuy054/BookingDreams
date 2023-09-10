using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreamsServices.Model;
using BookingDreamsServices.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;

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
        private readonly BookingDreamsServices.Services.IEmailService emailService;
       

        public TaiKhoanRes(UserManager<TaiKhoan> userManager, SignInManager<TaiKhoan> signInManager,RoleManager<IdentityRole> roleManager , IConfiguration configuration,
                            BookingDreamsContext context, IMapper mapper, BookingDreamsServices.Services.IEmailService emailService) {
            this.userManager = userManager; 
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.emailService = emailService;
            
        }
        public async Task<TaiKhoan> DangKi(DangKiModel dk,string role)
        {
            //Check user exist
            var userExist = await userManager.FindByEmailAsync(dk.Email);
            if(userExist != null)
            {
                var errors = new List<IdentityError>
                {
                    new IdentityError{Code ="Đăng kí không thàng công", Description = "Email đã tồn tại"  }
                };
                return new TaiKhoan();
            }

            //Check role exist
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                var errors = new List<IdentityError>
                {
                    new IdentityError{Code ="Đăng kí không thàng công", Description = "Role không tồn tại"  }
                };
                return new TaiKhoan();
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
            if (!result.Succeeded)
            {
                return new TaiKhoan();
            }
            //add role to user
            await userManager.AddToRoleAsync(user, role);
            return user;
        }
        

        //public async Task<string> ConfirmEmail(string token, string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    if(user != null)
        //    {
        //        var result = await userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return "Email verified successfully";
        //        }
        //    }
        //    //var message = new Message(new string[]
        //    //{
        //    //    "phamviabc2@gmail.com" }, 
        //    //    "Test",
        //    //    "<h1>Test Confirm Email</h1>"
        //    //);
        //    //emailService.SendEmail(message);
        //    return "User do not exist";
        //}

        public async Task<string> Update(DangKiModel model, string email)
        {
            if (model == null)
            {
                return "Update unsuccess";
            }
            if(model.Email != email)
            {
                return "User not exist";
            }
            var user = new TaiKhoan
            {
                HoTen = model.HoTen,
                NgaySinh = model.NgaySinh,
                CCCD = model.CCCD,
                GioiTinh = model.GioiTinh,
                PhoneNumber = model.SDT,
                DiaChi = model.DiaChi
            };
            await userManager.UpdateAsync(user);
            return "Update Success";
        }

        public async Task<UserModel> DangNhap(DangNhapModel dn)
        {
            //var result = await signInManager.PasswordSignInAsync(dn.Email, dn.Password, false, false);
            //if (!result.Succeeded)
            //{
            //    return string.Empty;
            //}
            var user = await userManager.FindByNameAsync(dn.Email);
            if(user != null && await userManager.CheckPasswordAsync(user,dn.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, dn.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    //new Claim(ClaimTypes.Email, dn.Email),
                };
                var userRole = await userManager.GetRolesAsync(user);
                foreach(var role in userRole)
                {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddMinutes(10),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                        );
                var infUser = new UserModel { 
                    HoTen = user.HoTen,
                    SDT = user.PhoneNumber,
                    NgaySinh = user.NgaySinh,
                    Email = user.Email,
                    CCCD = user.CCCD,
                    GioiTinh = user.GioiTinh,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
                return infUser;
            }
            return new UserModel();
        }
        public async Task<List<TaiKhoan>> GetAllKhachHang()
        {
            var lstUser = await userManager.GetUsersInRoleAsync("KhachHang");
            return (List<TaiKhoan>)lstUser;

        }
        public async Task<TaiKhoan> GetAllKhachHangByEmail(string email)
        {
            var lstUser = await userManager.GetUsersInRoleAsync("KhachHang");

            var user = lstUser.Where(x => x.Email == email).FirstOrDefault();
            return user;

        }

    }
}
