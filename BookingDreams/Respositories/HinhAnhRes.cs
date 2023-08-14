using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class HinhAnhRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public HinhAnhRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        //Get all
        public async Task<List<HinhAnhModel>> GetAll()
        {
            var hinhAnhs = await _context.HinhAnhs!.ToListAsync();
            return _mapper.Map<List<HinhAnhModel>>(hinhAnhs);
        }
        //Get one item
        public async Task<HinhAnhModel> GetByID(int id)
        {
            var hinhAnh = await _context.HinhAnhs!.FindAsync(id);
            return _mapper.Map<HinhAnhModel>(hinhAnh);
        }
        ////Add item
        //public async Task<int> Add(HinhAnhModel hinhAnh)
        //{
            
        //}
        //Update item

        //delete item
        public async Task Delete(int id)
        {
            var hinhAnh = _context.HinhAnhs!.SingleOrDefault(ha => ha.Id == id);
            if(hinhAnh != null)
            {
                _context.HinhAnhs!.Remove(hinhAnh);
                await _context.SaveChangesAsync();
                
            }
        }
    }
}
