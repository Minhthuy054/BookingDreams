using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyenController : ControllerBase
    {
        private readonly PhanQuyenRes _repo;

        public PhanQuyenController(PhanQuyenRes repo) { 
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanQuyenModel>> GetByID(int id)
        {
            var phanQuyen = await _repo.GetByID(id);
            return phanQuyen;
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhanQuyenModel phanQuyen)
        {
            if(phanQuyen == null)
            {
                return BadRequest();
            }
            await _repo.Add(phanQuyen);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(PhanQuyenModel phanQuyen, int id)
        {
            if(phanQuyen.IdQuyen != id)
            {
                return BadRequest();
            }
            await _repo.Update(phanQuyen, id);
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
