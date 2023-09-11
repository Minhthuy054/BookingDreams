using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingDreams.Controllers
{
    [Authorize(Roles = "NhanVien,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MaGiamGiaController : ControllerBase
    {
        private readonly MaGiamGiaRes _repo;

        public MaGiamGiaController(MaGiamGiaRes repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("GetByIdKS")]
        public async Task<ActionResult<MaGiamGiaModel>> GetByIdKS(int id)
        {
            var result = await _repo.GetByIdKS(id);
            return Ok(result);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MaGiamGiaModel model)
        {
            if (model == null) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Invalid Model" });
            }
            var result = await _repo.Add(model);
            return Ok(result);
            //return StatusCode(StatusCodes.Status200OK,
            //        new Response { Status = "Success", Message = result.ToString() });
        }
        [HttpPut]
        public async Task<IActionResult> Update(MaGiamGiaModel model, int id)
        {
            if (model.Id != id)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Invalid ID" });
            }
            if (model == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Invalid Model" });
            }
            var result = await _repo.Update(model, id);
            return Ok(result);
            //return StatusCode(StatusCodes.Status200OK,
            //       new Response { Status = "Success", Message = result.ToString() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(await _repo.GetAll() == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Not found item" });
            }
            var result = await _repo.Delete(id);
            return Ok(result);
            //return StatusCode(StatusCodes.Status200OK,
            //      new Response { Status = "Success", Message = result.ToString() });
        }
    }
}
