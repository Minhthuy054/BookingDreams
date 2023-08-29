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
            if (!resutl.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Error", Message = "Create UnSuccess" });
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
            //add token to verify email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "TaiKhoan", new { token, email = user.Email },Request.Scheme);
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
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
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
               new Response { Status = "Error", Message = "User not found" });
            }
            return Ok(result);
        }
        //[HttpGet("ConfirmEmail")]
        //public IActionResult ConfirmEmail()
        //{
        //    var result = _repo.ConfirmEmail();
        //    return StatusCode(StatusCodes.Status200OK,
        //            new Response { Status = "Success", Message = result });
        //}
        //[HttpPut]
        //public async Task<IActionResult> Update(TaiKhoanModel taiKhoan, int id)
        //{
        //    if (taiKhoan.Id != id)
        //    {
        //        return BadRequest();
        //    }
        //    await _repo.Update(taiKhoan, id);
        //    return Ok();
        //}
        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (_repo.GetAll() == null)
        //    {
        //        return BadRequest();
        //    }
        //    await _repo.Delete(id);
        //    return Ok();
        //}
        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(DangKiModel model, string email)
        {
            var result = await _repo.Update(model, email);
            return Ok(result);
        }
    }
}
