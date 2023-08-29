using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Composition.Convention;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhGiaController : ControllerBase
    {
        private readonly DanhGiaRes _repo;

        public DanhGiaController(DanhGiaRes repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("GetByID")]
        public async Task<ActionResult> GetByID(int id)
        {
            var danhGia = await _repo.GetByID(id);
            return Ok(danhGia);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DanhGiaModel model)
        {
            var result = await _repo.Add(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(DanhGiaModel model, int id)
        {
            var result = await _repo.Update(model, id);
            return Ok(result);
        }
    }
}
