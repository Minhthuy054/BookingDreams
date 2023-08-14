using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NuGet.LibraryModel;
using System.Runtime.CompilerServices;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanRes _repo;

        public TaiKhoanController(TaiKhoanRes repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoanModel>> GetByID(int id)
        {
            var taiKhoan = await _repo.GetByID(id);
            return taiKhoan;
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaiKhoanModel taiKhoan)
        {
            if(taiKhoan == null)
            {
                return BadRequest();
            }
            await _repo.Add(taiKhoan);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(TaiKhoanModel taiKhoan, int id)
        {
            if(taiKhoan.Id != id)
            {
                return BadRequest();
            }
            await _repo.Update(taiKhoan, id);
            return Ok();
        }
        [HttpDelete]
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
