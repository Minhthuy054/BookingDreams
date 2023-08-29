using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreams.Helpers;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace BookingDreams.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly PhongRes _repo;
        private readonly FuncSupport _funcSupport;

        public PhongController(PhongRes repo,FuncSupport funcSupport) {
            _repo = repo;
            _funcSupport = funcSupport;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongModel>> GetByID(int id)
        {
            var phong = await _repo.GetByID(id);
            return phong;
        }
        [HttpPost]
        public async Task<IActionResult> Create ([FromForm] PhongImge phong)
        {
            
            if(phong == null)
            {
                return BadRequest();
            }

            string lstLink = "";
            var newPhong = new PhongModel
            {
                IdKhachSan = phong.IdKhachSan,
                TenPhong = phong.TenPhong,
                SoPhong = phong.SoPhong,
                GiaPhong = phong.GiaPhong,
                MoTa = phong.MoTa,
                Loai = phong.Loai,
                SoLuongNguoiLon = phong.SoLuongNguoiLon,
                SoLuongTreEm = phong.SoLuongTreEm
            };

            if (phong.HinhAnhFile.Count() > 0)
            {
                
                foreach (var file in phong.HinhAnhFile)
                {
                    if (!_funcSupport.IsImageFile(file))
                    {
                        return BadRequest("Tệp đầu vào không phải là ảnh");
                    }
                    DateTime date = DateTime.Now;
                    string publishPath = Path.Combine(@"images", "phong", date.ToString("yyyy-MM-dd"));
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
                    string filePath = "Phong" + "_"   + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
                    //Lưu file vào hệ thống
                    using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    lstLink += Path.Combine(path, filePath) + ";";
                }
            }
            
            newPhong.HinhAnh = lstLink;
            await _repo.Add(newPhong);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PhongImge phong, int id) { 
            if(phong.Id != id)
            {
                return BadRequest();
            }
            string lstLink = "";
            var newPhong = new PhongModel
            {
                IdKhachSan = phong.IdKhachSan,
                TenPhong = phong.TenPhong,
                SoPhong = phong.SoPhong,
                GiaPhong = phong.GiaPhong,
                MoTa = phong.MoTa,
                Loai = phong.Loai,
                SoLuongNguoiLon = phong.SoLuongNguoiLon,
                SoLuongTreEm = phong.SoLuongTreEm
            };
            if (phong.HinhAnhFile.Count() > 0)
            {
                foreach (var file in phong.HinhAnhFile)
                {
                    DateTime date = DateTime.Now;
                    string publishPath = Path.Combine(@"images", "phong", date.ToString("yyyy-MM-dd"));
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
                    string filePath = "Phong" + "_" + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
                    //Lưu file vào hệ thống
                    using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    lstLink += Path.Combine(path, filePath) + ";";
                }
            }
            newPhong.HinhAnh = lstLink;
            await _repo.Update(newPhong, id);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return BadRequest();
            }
            await _repo.Delete(id);
            return Ok();
        }
    }
}
