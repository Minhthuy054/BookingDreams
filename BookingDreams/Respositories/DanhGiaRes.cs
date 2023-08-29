using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace BookingDreams.Respositories
{
    public class DanhGiaRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public DanhGiaRes(BookingDreamsContext context, IMapper maper) {
            _context = context;
            _mapper = maper;
        }
        public async Task<List<DanhGiaModel>> GetAll()
        {
            var lstDanhGia = await _context.DanhGias!.ToListAsync();
            return _mapper.Map<List<DanhGiaModel>>(lstDanhGia);
        }
        public async Task<DanhGiaModel> GetByID(int id)
        {
            var danhGia = await _context.DanhGias!.FindAsync(id);
            return _mapper.Map<DanhGiaModel>(danhGia);
        }
        public async Task<bool> Add(DanhGiaModel model)
        {
            
            if (model == null)
            {
                return false;
            }
            var newModel = _mapper.Map<DanhGia>(model);
            _context.DanhGias!.Add(newModel);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(DanhGiaModel model, int id)
        {
            if (model == null)
            {
                return false;
            }
            if (model.Id != id)
            {
                return false;
            }
            var newModel = _mapper.Map<DanhGia>(model);
            _context.DanhGias!.Update(newModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
