using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class PhanQuyenRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public PhanQuyenRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PhanQuyenModel>> GetAll()
        {
            var phanQuyens = await _context.PhanQuyens!.ToListAsync();
            return _mapper.Map<List<PhanQuyenModel>>(phanQuyens);
        }
        public async Task<PhanQuyenModel> GetByID(int id)
        {
            var phanQuyen = await _context.PhanQuyens!.FindAsync(id);
            return _mapper.Map<PhanQuyenModel>(phanQuyen);
        }
        public async Task<int> Add(PhanQuyenModel phanQuyen)
        {
            var newPhanQuyen = _mapper.Map<PhanQuyen>(phanQuyen);
            _context.PhanQuyens!.Add(newPhanQuyen);
            await _context.SaveChangesAsync();
            return newPhanQuyen.Id;
        }
        public async Task Update(PhanQuyenModel phanQuyen, int id)
        {
            if(phanQuyen.Id == id)
            {
                var newPhanQuyen = _mapper.Map<PhanQuyen>(phanQuyen);
                _context.PhanQuyens!.Update(newPhanQuyen);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var phanQuyen = _context.PhanQuyens!.SingleOrDefault(pq => pq.Id == id);
            if(phanQuyen != null)
            {
                _context.PhanQuyens!.Remove(phanQuyen);
                await _context.SaveChangesAsync();
            }
        }
    }
}
