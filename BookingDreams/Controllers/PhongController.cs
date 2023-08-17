using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly PhongRes _repo;
        private bool IsImageFile(IFormFile file)
        {
            try
            {
                using (var img = Image.FromStream(file.OpenReadStream()))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public PhongController(PhongRes repo) {
            _repo = repo;
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
                TrangThai = phong.TrangThai,
                GiaPhong = phong.GiaPhong,
                IdLoai = phong.IdLoai,
                Active = phong.Active
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
                    string filePath = "Phong" + "_"   + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
                    //Lưu file vào hệ thống
                    using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    lstLink += Path.Combine(path, filePath) + ";";
                }
            }
            //if (!IsImageFile(phong.HinhAnhFile))
            //{
            //    return BadRequest("Tệp đầu vào không phải là ảnh");
            //}
            newPhong.HinhAnh = lstLink;
            await _repo.Add(newPhong);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(PhongModel phong, int id) { 
            if(phong.Id != id)
            {
                return BadRequest();
            }
            await _repo.Update(phong, id);
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
