using BookingDreams.Models;
using BookingDreams.Respositories;
using MessagePack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly ThanhToanRes _repo;

        public ThanhToanController(ThanhToanRes repo) {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ThanhToanModel>> GetByID(int id)
        {
            var thanhToan = await _repo.GetByID(id);
            return thanhToan;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ThanhToanModel thanhToan)
        {
            if(thanhToan == null)
            {
                return BadRequest();
            }
            await _repo.Add(thanhToan);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(ThanhToanModel thanhToan, int id)
        {
            if(thanhToan.Id != id)
            {
                return BadRequest();
            }
            await _repo.Update(thanhToan,id);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return NotFound();
            }
            await _repo.Delete(id);
            return Ok();
        }
    }   
}
