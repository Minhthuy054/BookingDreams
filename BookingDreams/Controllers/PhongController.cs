using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : ControllerBase
    {
        private readonly PhongRes _repo;

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
        public async Task<IActionResult> Create (PhongModel phong)
        {
            if(phong == null)
            {
                return BadRequest();
            }
            if(phong.Id != null)
            {
                return Ok("Phong da ton tai");    
            }
            await _repo.Add(phong);
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
