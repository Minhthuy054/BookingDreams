using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class ThanhToanRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public ThanhToanRes(BookingDreamsContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ThanhToanModel>> GetAll()
        {
            var thanhToans = await _context.ThanhToans!.ToListAsync();
            return _mapper.Map<List<ThanhToanModel>>(thanhToans);
        }
        public async Task<ThanhToanModel> GetByID(int id)
        {
            var thanhToan = await _context.ThanhToans!.FindAsync(id);
            return _mapper.Map<ThanhToanModel>(thanhToan);
        }
        public async Task<int> Add(ThanhToanModel thanhToan)
        {
            var newThanhToan = _mapper.Map<ThanhToan>(thanhToan);
            _context.ThanhToans!.Add(newThanhToan);
            await _context.SaveChangesAsync();
            return newThanhToan.Id;
        }
        public async Task Update(ThanhToanModel thanhToan, int id)
        {
            if(thanhToan.Id == id)
            {
                var newThanhToan = _mapper.Map<ThanhToan>(thanhToan);
                _context.ThanhToans!.Update(newThanhToan);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var thanhToan = _context.ThanhToans!.SingleOrDefault(tt => tt.Id == id);
            _context.ThanhToans!.Remove(thanhToan);
            await _context.SaveChangesAsync();
        }
    }
}
