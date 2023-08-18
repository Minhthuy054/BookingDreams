using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookingDreams.Respositories
{
    public class TinhThanhRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public TinhThanhRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TinhThanhModel>> GetAll()
        {
            var tinhThanhs = await _context.TinhThanhs!.ToListAsync();
            return _mapper.Map<List<TinhThanhModel>>(tinhThanhs);
        }
        public async Task<TinhThanhModel> GetByID(int id)
        {
            var tinhThanh = await _context.TinhThanhs!.FindAsync(id);
            return _mapper.Map<TinhThanhModel>(tinhThanh);
        }
        public async Task<int> Add(TinhThanhModel tinhThanh)
        {
            var newTinhThanh = _mapper.Map<TinhThanh>(tinhThanh);
            _context.TinhThanhs!.Add(newTinhThanh);
            await _context.SaveChangesAsync();
            return newTinhThanh.Id;
        }
        public async Task Update(TinhThanhModel tinhThanh, int id)
        {
            if(tinhThanh.Id == id)
            {
                var newTinhThanh = _mapper.Map<TinhThanh>(tinhThanh);
                _context.TinhThanhs!.Update(newTinhThanh);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var tinhThanh = _context.TinhThanhs!.SingleOrDefault(tt =>tt.Id == id);
            if(tinhThanh != null)
            {
                _context.TinhThanhs!.Remove(tinhThanh);
                await _context.SaveChangesAsync();
            }
        }
    }
}
