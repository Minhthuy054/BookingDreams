using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhThanhController : ControllerBase
    {
        private readonly TinhThanhRes _repo;

        public TinhThanhController(TinhThanhRes repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<TinhThanhModel>>> GetAll() {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TinhThanhModel>> GetByID(int id)
        {
            var tinhThanh = await _repo.GetByID(id);
            return Ok(tinhThanh);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TinhThanhModel tinhThanh)
        {
            if(tinhThanh == null)
            {
                return BadRequest();
            }
            await _repo.Add(tinhThanh);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(TinhThanhModel tinhThanh, int id)
        {
            if(tinhThanh.Id != id)
            {
                Ok("Không tồn tại tỉnh thành");
            }
            await _repo.Update(tinhThanh, id);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return Ok("Chưa có tỉnh thành nào");
            }
            await _repo.Delete(id);
            return Ok();
        }
    }
}
