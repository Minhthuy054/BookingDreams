using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class KhachHangRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public KhachHangRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        //Get all
        public async Task<List<KhachHangModel>> GetAll()
        {
            var khachHangs = await _context.KhachHangs!.ToListAsync();
            return _mapper.Map<List<KhachHangModel>>(khachHangs);
        }
        //Get item
        public async Task<KhachHangModel> GetByID(int id)
        {
            var khachHang = await _context.KhachHangs!.FindAsync(id);
            return _mapper.Map<KhachHangModel>(khachHang);
        }
        //Add
        public async Task<int> Add(KhachHangModel khachHang)
        {
            var newKhachHang = _mapper.Map<KhachHang>(khachHang);
            _context.KhachHangs!.Add(newKhachHang);
            await _context.SaveChangesAsync();
            return newKhachHang.Id;
        }
        //Update
        public async Task<bool> Update(KhachHangModel khachHang, int id)
        {
            if(khachHang.Id == id)
            {
                var newkhachHang = _mapper.Map<KhachHang>(khachHang);
                _context.KhachHangs!.Update(newkhachHang);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        //delete khachhang
        public async Task<bool> Delete(int id)
        {
            var khachHang = _context.KhachHangs!.SingleOrDefault(kh => kh.Id == id);
            if(khachHang != null)
            {
                _context.KhachHangs!.Remove(khachHang);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
