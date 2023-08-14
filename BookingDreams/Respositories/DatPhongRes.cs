using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class DatPhongRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public DatPhongRes(BookingDreamsContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }
        //Lấy tất cả
        public async Task<List<DatPhongModel>> GetAll()
        {
            var datPhongs = await _context.DatPhongs!.ToListAsync();
            return _mapper.Map<List<DatPhongModel>>(datPhongs);
        }
        //Lấy 1 item
        public async Task<DatPhongModel> GetByID(int id)
        {
            var datPhong = await _context.DatPhongs!.FindAsync(id);
            return _mapper.Map<DatPhongModel>(datPhong);
        }
        //Thêm mới 1 item
        public async Task<int> Add(DatPhongModel datPhong)
        {
            var newDatPhong = _mapper.Map<DatPhong>(datPhong);
            _context.DatPhongs!.Add(newDatPhong);
            await _context.SaveChangesAsync();
            return newDatPhong.Id;
        }
        //Sửa item
        public async Task Update(DatPhongModel datPhong, int id)
        {
            if(datPhong.Id == id)
            {
                var updateDatPhong = _mapper.Map<DatPhong>(datPhong);
                _context.DatPhongs!.Update(updateDatPhong);
                await _context.SaveChangesAsync();
            }

        }
        //Xóa item
        public async Task Delete(int id)
        {
            var datPhong = _context.DatPhongs!.SingleOrDefault(dp => dp.Id == id);
            if(datPhong != null)
            {
                _context.DatPhongs!.Remove(datPhong);
                await _context.SaveChangesAsync();
            }
        }
    }
}
