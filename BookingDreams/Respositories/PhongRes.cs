using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Org.BouncyCastle.Crypto;
using System.Data;
using System.IO;

using Microsoft.ML.Data;


namespace BookingDreams.Respositories
{
    public class PhongRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhongRes(BookingDreamsContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<PhongModel>> GetAll()
        {
            var phongs = await _context.Phongs!.ToListAsync();
            return _mapper.Map<List<PhongModel>>(phongs);
        }
        public async Task<PhongModel> GetByID(int id)
        {
            var phong = await _context.Phongs!.FindAsync(id);
            return _mapper.Map<PhongModel>(phong);
        }
        public async Task<int> Add(PhongModel phong )
        {
            var newPhong = _mapper.Map<Phong>(phong);
            _context.Add(newPhong);
            await _context.SaveChangesAsync();
            return newPhong.Id;
        }
        public async Task Update(PhongModel phong, int id)
        {
            var newPhong = _mapper.Map<Phong>(phong);
            _context.Update(newPhong);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var phong = _context.Phongs!.SingleOrDefault(p => p.Id == id);
            if(phong != null)
            {
                _context.Phongs!.Remove(phong);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<PhongModel>> GetByKhachSan(int id)
        {
            var phongs = await _context.Phongs!.ToListAsync();
            var lstPhong =  _mapper.Map<List<PhongModel>>(phongs);
            lstPhong = lstPhong.Where(p => p.IdKhachSan == id).ToList();
            return lstPhong;
        }
    }
}
