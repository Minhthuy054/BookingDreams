using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingDreams.Data;
using BookingDreams.Respositories;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using BookingDreams.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BookingDreams.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class KhachSansController : ControllerBase
    {
        private readonly BookingDreamsContext _context;
        private readonly KhachSanRes _repo;
        private readonly FuncSupport _funcSupport;

        public KhachSansController(BookingDreamsContext context, KhachSanRes repo, FuncSupport funcSupport)
        {
            _context = context;
            _repo = repo;
            _funcSupport = funcSupport;
        }

        // GET: api/KhachSans
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<KhachSan>>> GetKhachSans()
        //{
        //    if (_context.KhachSans == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.KhachSans.ToListAsync();
        //}
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        // GET: api/KhachSans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachSanModel>> GetKhachSan(int id)
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
            var khachSan = await _repo.GetByID(id);//_context.KhachSans.FindAsync(id);

            if (khachSan == null)
            {
                return NotFound();
            }

            return khachSan;
        }

        // PUT: api/KhachSans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachSan(int id, KhachSanModel khachSan)
        {

            if (id != khachSan.Id)
            {
                return BadRequest();
            }

            //_context.Entry(khachSan).State = EntityState.Modified;

            try
            {
                //string lstLink = "";
                //string link = "";
                //var newKhachSan = new KhachSanModel
                //{
                //    IdTinhThanh = khachSanImg.IdTinhThanh,
                //    MaKhachSan = khachSanImg.MaKhachSan,
                //    TenKhachSan = khachSanImg.TenKhachSan,
                //    DiaChi = khachSanImg.DiaChi,
                //    GioiThieu = khachSanImg.GioiThieu,
                //    TieuDe = khachSanImg.TieuDe,
                //    GhiChu = khachSanImg.GhiChu
                //};

                //if (khachSanImg.HinhAnhFile.Count() > 0)
                //{

                //    foreach (var file in khachSanImg.HinhAnhFile)
                //    {
                //        if (!_funcSupport.IsImageFile(file))
                //        {
                //            return BadRequest("Tệp đầu vào không phải là ảnh");
                //        }
                //        DateTime date = DateTime.Now;
                //        string publishPath = Path.Combine(@"images", "KhachSan_"+ newKhachSan.TenKhachSan, date.ToString("yyyy-MM-dd"));
                //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
                //        if (!Directory.Exists(path))
                //        {
                //            Directory.CreateDirectory(path);
                //        }
                //        string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
                //        string filePath = "KhachSan" + "_" + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
                //                                                                                                       //Lưu file vào hệ thống
                //        using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
                //        {
                //            await file.CopyToAsync(fileStream);
                //        }
                //        link = Path.Combine(publishPath, filePath);
                //        link = link.Replace("\\", "/");
                //        lstLink += link + ";";
                //    }
                //}
                //newKhachSan.HinhAnh = lstLink;


                await _repo.Update(khachSan, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachSanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/KhachSans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [Authorize]
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpPost]
        public async Task<ActionResult<KhachSanModel>> PostKhachSan(/*[FromForm]*/ KhachSanModel khachSan)
        {
          if (_context.KhachSans == null)
          {
              return Problem("Entity set 'BookingDreamsContext.KhachSans'  is null.");
          }
            //string lstLink = "";
            //string link = "";
            //var newKhachSan = new KhachSanModel
            //{
            //    IdTinhThanh = khachSan.IdTinhThanh,
            //    MaKhachSan = khachSan.MaKhachSan,
            //    TenKhachSan = khachSan.TenKhachSan,
            //    DiaChi = khachSan.DiaChi,
            //    GioiThieu = khachSan.GioiThieu,
            //    TieuDe = khachSan.TieuDe,
            //    GhiChu = khachSan.GhiChu
            //};

            //if (khachSan.HinhAnhFile.Count() > 0)
            //{

            //    foreach (var file in khachSan.HinhAnhFile)
            //    {
            //        if (!_funcSupport.IsImageFile(file))
            //        {
            //            return BadRequest("Tệp đầu vào không phải là ảnh");
            //        }
            //        DateTime date = DateTime.Now;
            //        string publishPath = Path.Combine(@"images", "KhachSan_" + newKhachSan.TenKhachSan, date.ToString("yyyy-MM-dd"));
            //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", publishPath);//Đường dẫn để lưu file
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }
            //        string fileExtension = new FileInfo(file.FileName).Extension; //Định dạng của file (png,jpg,...)
            //        string filePath = "KhachSan" + "_" + date.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName; //Tên file lưu vào hệ thống
            //        //Lưu file vào hệ thống
            //        using (var fileStream = new FileStream(Path.Combine(path, filePath), FileMode.Create))
            //        {
            //            await file.CopyToAsync(fileStream);
            //        }
            //        link = Path.Combine(publishPath, filePath);
            //        link = link.Replace("\\","/");
            //        lstLink += link + ";";
            //    }
            //}
            //newKhachSan.HinhAnh = lstLink;
            var result = await _repo.Add(khachSan);

            return Ok(result);
        }

        // DELETE: api/KhachSans/5
        [Authorize(Roles = "NhanVien,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachSan(int id)
        {
            if (_context.KhachSans == null)
            {
                return NotFound();
            }
            var khachSan = await _context.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return NotFound();
            }

            //_context.KhachSans.Remove(khachSan);
           var result = await _repo.Delete(id); //_context.SaveChangesAsync();

            return Ok(result);
        }

        private bool KhachSanExists(int id)
        {
            return (_context.KhachSans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
