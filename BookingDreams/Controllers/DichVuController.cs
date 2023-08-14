using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private DichVuRes _repo;

        public DichVuController(DichVuRes repo) { 
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVuModel>> GetByID(int id)
        {
            var dichVu = await _repo.GetByID(id);
            return Ok(dichVu);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DichVuModel dichVu)
        {
            if(dichVu == null)
            {
                return BadRequest();
            }
            await _repo.Add(dichVu);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(DichVuModel dichVu, int id)
        {
            if(dichVu.Id != id)
            {
                return BadRequest();
            }
            await _repo.Update(dichVu, id);
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
