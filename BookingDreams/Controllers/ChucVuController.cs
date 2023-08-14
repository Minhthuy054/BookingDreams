using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingDreams.Data;
using BookingDreams.Respositories;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace BookingDreams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVuController : ControllerBase
    {
        private ChucVuRes _repo;

        public ChucVuController(ChucVuRes repo) {
            _repo = repo;
        }
        //Lấy tất cả các item
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _repo.GetAll());
        }

        //thêm mới
        [HttpPost]
        public async Task<IActionResult> Create(ChucVuModel chucVu)
        {
            if (chucVu == null)
            {
                return BadRequest();
            }
            await _repo.Add(chucVu);
            return Ok();
        }
        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ChucVuModel chucVu, int id)
        {
            if(id != chucVu.Id)
            {
                return BadRequest();
            }
            await _repo.Update(chucVu, id);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChucVuModel>> GetByID(int id)
        {
            var chucVu =  await _repo.GetByID(id);
            return chucVu;
        }
        [HttpDelete("{id}")]
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
