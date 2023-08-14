using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace BookingDreams.Respositories
{
    public class RoleRes
    {
        private readonly BookingDreamsContext _context;
        private readonly IMapper _mapper;

        public RoleRes(BookingDreamsContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<RoleModel>> GetAll()
        {
            var roles = await _context.Roles!.ToListAsync();
            return _mapper.Map<List<RoleModel>>(roles);
        }
        public async Task<RoleModel> GetByID(int id)
        {
            var role = await _context.Roles!.FindAsync(id);
            return _mapper.Map<RoleModel>(role);
        }
        public async Task<int> Add(RoleModel role)
        {
            var newRole = _mapper.Map<Role>(role);
            _context.Roles!.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole.Id;
        }
        public async Task Update(RoleModel role, int id)
        {
            if(role.Id == id)
            {
                var newRole = _mapper.Map<Role>(role);
                _context.Roles!.Update(newRole);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var phong = _context.Roles!.SingleOrDefault(r => r.Id == id);
            if(phong != null)
            {
                _context.Roles!.Remove(phong);
                await _context.SaveChangesAsync();
            }
        }
    }
}
