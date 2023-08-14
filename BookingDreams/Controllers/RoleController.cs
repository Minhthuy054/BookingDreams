using BookingDreams.Models;
using BookingDreams.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualBasic;
using System.Drawing.Printing;

namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRes _repo;

        public RoleController(RoleRes repo) {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetByID(int id)
        {
            var role = await _repo.GetByID(id);
            return role;
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleModel role)
        {
            if(role == null)
            {
                return BadRequest();
            }
            await _repo.Add(role);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(RoleModel role, int id)
        {
            if(role.Id != id)
            {
                return BadRequest();
            }
            await _repo.Update(role,id);
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
