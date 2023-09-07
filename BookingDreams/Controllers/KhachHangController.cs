using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangRes _repo;

        public KhachHangController(KhachHangRes repo) { 
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHangModel>> GetByID(int id)
        {
            var khachHang = await _repo.GetByID(id);
            return khachHang;
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhachHangModel khachHang)
        {
            if (khachHang == null)
            {
                return BadRequest();
            }
            var result = await _repo.Add(khachHang);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(KhachHangModel khachHang, int id)
        {
            if(khachHang.Id != id)
            {
                return BadRequest();
            }
            var result = await _repo.Update(khachHang, id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return BadRequest();
            }
            var result = await _repo.Delete(id);
            return Ok(result);
        }

    }
}
