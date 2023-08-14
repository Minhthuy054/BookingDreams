using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class TaiKhoanRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public TaiKhoanRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TaiKhoanModel>> GetAll()
        {
            var taiKhoans = await _context.TaiKhoans!.ToListAsync();
            return _mapper.Map<List<TaiKhoanModel>>(taiKhoans);
        }
        public async Task<TaiKhoanModel> GetByID(int id)
        {
            var taiKhoan = await _context.TaiKhoans!.FindAsync(id);
            return _mapper.Map<TaiKhoanModel>(taiKhoan);
        }
        public async Task<int> Add(TaiKhoanModel taiKhoan)
        {
            var newTaiKhoan = _mapper.Map<TaiKhoan>(taiKhoan);
            _context.TaiKhoans!.Add(newTaiKhoan);
            await _context.SaveChangesAsync();
            return newTaiKhoan.Id;
        }
        public async Task Update(TaiKhoanModel taiKhoan, int id)
        {
            if(taiKhoan.Id == id)
            {
                var newTaiKhoan = _mapper.Map<TaiKhoan>(taiKhoan);
                _context.TaiKhoans!.Update(newTaiKhoan);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var taiKhoan = _context.TaiKhoans!.SingleOrDefault(x => x.Id == id);
            if(taiKhoan != null)
            {
                _context.TaiKhoans!.Remove(taiKhoan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
