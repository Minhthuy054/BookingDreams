using BookingDreams.Data;
using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatPhongController : ControllerBase
    {
        private DatPhongRes _repo;

        public DatPhongController(DatPhongRes repo) { 
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DatPhongModel>> GetByID(int id)
        {
            var datPhong = await _repo.GetByID(id);
            return datPhong;
        }
        [HttpPost]
        public async Task<IActionResult> Create(DatPhongModel datPhong)
        {
            if(datPhong == null)
            {
                return BadRequest();
            }
            var result = await _repo.Add(datPhong);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DatPhongModel datPhong)
        {
            if(datPhong.Id != id)
            {
                return BadRequest();
            }
            var result = await _repo.Update(datPhong,id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(_repo.GetAll() == null)
            {
                return NotFound();
            }
            var result = await _repo.Delete(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _repo.GetByEmail(email));
        }
    }
}
