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
    public class KhachSansController : ControllerBase
    {
        private readonly BookingDreamsContext _context;
        private readonly KhachSanRes _repo;

        public KhachSansController(BookingDreamsContext context, KhachSanRes repo)
        {
            _context = context;
            _repo = repo; 
        }

        // GET: api/KhachSans
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<KhachSan>>> GetKhachSans()
        //{
        //    if (_context.KhachSans == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.KhachSans.ToListAsync();
        //}
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        // GET: api/KhachSans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachSanModel>> GetKhachSan(int id)
        {
          if (_context.KhachSans == null)
          {
              return NotFound();
          }
            var khachSan = await _repo.GetByID(id);//_context.KhachSans.FindAsync(id);

            if (khachSan == null)
            {
                return NotFound();
            }

            return khachSan;
        }

        // PUT: api/KhachSans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachSan(int id, KhachSanModel khachSan)
        {
            if (id != khachSan.Id)
            {
                return BadRequest();
            }

            //_context.Entry(khachSan).State = EntityState.Modified;

            try
            {
                await _repo.Update(khachSan,id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachSanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KhachSans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachSanModel>> PostKhachSan(KhachSanModel khachSan)
        {
          if (_context.KhachSans == null)
          {
              return Problem("Entity set 'BookingDreamsContext.KhachSans'  is null.");
          }
            //_context.KhachSans.Add(khachSan);
            //await _context.SaveChangesAsync();
            await _repo.Add(khachSan);

            return CreatedAtAction("GetKhachSan", new { id = khachSan.Id }, khachSan);
        }

        // DELETE: api/KhachSans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachSan(int id)
        {
            if (_context.KhachSans == null)
            {
                return NotFound();
            }
            var khachSan = await _context.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return NotFound();
            }

            //_context.KhachSans.Remove(khachSan);
            await _repo.Delete(id); //_context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachSanExists(int id)
        {
            return (_context.KhachSans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
