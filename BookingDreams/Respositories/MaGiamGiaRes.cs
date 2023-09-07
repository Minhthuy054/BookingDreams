using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class MaGiamGiaRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public MaGiamGiaRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        //get all MaGiamGia
        public async Task<List<MaGiamGiaModel>> GetAll()
        {
            var lstMaGiamGia = await _context.MaGiamGias!.ToListAsync();
            return _mapper.Map<List<MaGiamGiaModel>>(lstMaGiamGia);
        }
        //Get by ID
        public async Task<List<MaGiamGiaModel>> GetByIdKS(int id)
        {
            var maGiamGia = await _context.MaGiamGias!.ToListAsync();
            maGiamGia = maGiamGia.Where(m => m.IdKhachSan == id).ToList();
            return _mapper.Map<List<MaGiamGiaModel>>(maGiamGia);
        }
        public async Task<bool> Add(MaGiamGiaModel model)
        {
            var newModel = _mapper.Map<MaGiamGia>(model);
            _context.MaGiamGias!.Add(newModel);
            var result = await _context.SaveChangesAsync();
            if(result == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Update (MaGiamGiaModel model,int id)
        {
            if(model.Id != id)
            {
                return false;
            }
            var newModel = _mapper.Map<MaGiamGia>(model);
            _context.MaGiamGias!.Update(newModel);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var maGiamGia =  _context.MaGiamGias!.SingleOrDefault(x => x.Id == id); 
            if(maGiamGia == null)
            {
                return false;
            }
            _context.MaGiamGias!.Remove(maGiamGia);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
