using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace BookingDreams.Respositories
{
    public class KhachSanRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public KhachSanRes(BookingDreamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<KhachSanModel>> GetAll()
        {
            var khachSans = await _context.KhachSans!.ToListAsync();
            return _mapper.Map<List<KhachSanModel>>(khachSans);
        }
        public async Task<KhachSanModel> GetByID(int id)
        {
            var khachSan = await _context.KhachSans!.FindAsync(id);
            return _mapper.Map<KhachSanModel>(khachSan);
        }
        public async Task<int> Add(KhachSanModel khachSan)
        {
            var newKhachSan = _mapper.Map<KhachSan>(khachSan);
            _context.KhachSans!.Add(newKhachSan);
            await _context.SaveChangesAsync();
            return newKhachSan.Id;
        }
        public async Task Update(KhachSanModel khachSan, int id)
        {
            if (id == khachSan.Id)
            {
                var update = _mapper.Map<KhachSan>(khachSan);
                _context.KhachSans!.Update(update);
                await _context.SaveChangesAsync();

            }

        }
        public async Task Delete(int id)
        {
            var delete = _context.KhachSans!.SingleOrDefault(ks => ks.Id == id);
            if (delete != null)
            {
                _context.KhachSans!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
