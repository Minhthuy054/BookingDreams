using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreams.Respositories;
using BookingDreamsServices.Model;
using BookingDreamsServices.Services;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NuGet.Common;
using NuGet.LibraryModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanRes _repo;
        private readonly BookingDreamsServices.Services.IEmailService _emailService;
        private readonly UserManager<TaiKhoan> _userManager;

        public TaiKhoanController(TaiKhoanRes repo, BookingDreamsServices.Services.IEmailService emailService,UserManager<TaiKhoan> userManager)
        {
            _repo = repo;
            _emailService = emailService;
            _userManager = userManager;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _repo.GetAll());
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TaiKhoanModel>> GetByID(int id)
        //{
        //    var taiKhoan = await _repo.GetByID(id);
        //    return taiKhoan;
        //}
        [HttpPost("DangKi")]
        public async Task<IActionResult> DangKi([FromBody ]DangKiModel dk,string role)
        {
            var resutl = await _repo.DangKi(dk,role);
            // var confirmationLink = UrlHelperExtensions.Action(nameof(ConfirmEmail), "TaiKhoan", new { token, email = user.Email }, Request.Scheme);
            if (resutl.Id == null)
            {
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Error", Message = "Create UnSuccess" });
            }
            
            //add token to verify email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(resutl);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "TaiKhoan", new { token, email = resutl.Email },Request.Scheme);
            var message = new Message(new string[] { resutl.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                       new Response { Status = "Success", Message = $"User created and sent email verify and token: {token}" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            //var result = await _repo.ConfirmEmail(token, email);
            //return StatusCode(StatusCodes.Status200OK,
            //    new Response { Status = "Success", Message = result });
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Error", Message = "User do not exist" });
        }
            
        [HttpPost("DangNhap")]
        public async Task<IActionResult> DangNhap(DangNhapModel dangNhap)
        {
            var result = await _repo.DangNhap(dangNhap);
            if (result.Token == null)
            {
                return Ok(false);
            }
            return Ok(result);
        }
        
        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(DangKiModel model, string email)
        {
            var result = await _repo.Update(model, email);
            return Ok(result);
        }
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpGet("GetAllKhachHang")]
        public async Task<IActionResult> GetAllKhachHang()
        {
            var lstKhachHang = await _repo.GetAllKhachHang();
            return Ok(lstKhachHang);
        }
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpGet("GetKhachHangByEmail")]
        public async Task<IActionResult> GetKhachHangByEmail(string email)
        {
            var khachHang = await _repo.GetAllKhachHangByEmail(email);
            return Ok(khachHang);
        }

        [HttpPost("Forgot-Password")]
        
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = Url.Action(nameof(ResetPassword), "TaiKhoan", new{token, email = user.Email},Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password link", forgotPasswordLink!);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = $"Password Changed request is sent on Email: {user.Email}. Please Open your email and click link" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Could not send link to Email. Try again" });
        }
        [HttpGet("Reset-Password")]
        public async Task<IActionResult> ResetPassword(string token,string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new { model });
        }

        [HttpPost("Reset-Password")]
       // [Route("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPasswordResult.Succeeded)
                {
                    foreach(var error in resetPasswordResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = "Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = $"Could not send link to Email. Try again" });
        }
    }
}
