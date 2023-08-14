using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace BookingDreams.Respositories
{
    public class ChucVuRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public ChucVuRes(BookingDreamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Lấy tất cả
        public async Task<List<ChucVuModel>> GetAll()
        {
            var chucVus = await _context.ChucVus!.ToListAsync();
            return _mapper.Map<List<ChucVuModel>>(chucVus);
        }
        //Lấy theo id
        public async Task<ChucVuModel> GetByID(int id)
        {
            var chucVu = await _context.ChucVus!.FindAsync(id);
            return _mapper.Map<ChucVuModel>(chucVu);
        }
        //Thêm mới chức vụ
        public async Task<int> Add(ChucVuModel chucVu)
        {
            var newChucVu = _mapper.Map<ChucVu>(chucVu);
            _context.ChucVus!.Add(newChucVu);
            await _context.SaveChangesAsync();
            return newChucVu.Id;
        }
        //update chức vụ
        public async Task Update(ChucVuModel chucVu, int id)
        {
            if (chucVu.Id == id)
            {
                var newChucVu = _mapper.Map<ChucVu>(chucVu);
                _context.ChucVus!.Update(newChucVu);
                await _context.SaveChangesAsync();
            }
        }
        //Xóa chức vụ
        public async Task Delete(int id)
        {
            var chucVu = _context.ChucVus!.SingleOrDefault(cv => cv.Id == id);
            if(chucVu != null)
            {
                _context.ChucVus!.Remove(chucVu);
                await _context.SaveChangesAsync();
            }
        }
    }
}

