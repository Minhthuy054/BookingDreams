using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class DichVuRes
    {
        private BookingDreamsContext _context;
        private IMapper _mapper;

        public DichVuRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        //GetAll item
        public async Task<List<DichVuModel>> GetAll()
        {
            var dichVus = await _context.DichVus!.ToListAsync();
            return _mapper.Map<List<DichVuModel>>(dichVus);
        }
        //Get one item
        public async Task<DichVuModel> GetByID(int id)
        {
            var dichVu = await _context.DichVus!.FindAsync(id);
            return _mapper.Map<DichVuModel>(dichVu);
        }
        //Add item
        public async Task<int> Add(DichVuModel dichVu)
        {
            var newDichVu = _mapper.Map<DichVu>(dichVu);
            _context.DichVus!.Add(newDichVu);
            await _context.SaveChangesAsync();
            return newDichVu.Id;   
        }
        //Update item
        public async Task Update(DichVuModel dichVu, int id)
        {
            var newDichVu = _mapper.Map<DichVu>(dichVu);
            if(dichVu.Id == id)
            {
                _context.Update(newDichVu);
                await _context.SaveChangesAsync();
            }
        }
        //Delte item
        public async Task Delete(int id)
        {
            var dichVu =  _context.DichVus!.SingleOrDefault(dv => dv.Id == id);
            if(dichVu != null)
            {
                _context.DichVus!.Remove(dichVu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
