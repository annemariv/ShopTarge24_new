using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenService : IKindergartenService
    {
        private readonly ShopTARge24Context _context;

        public KindergartenService(ShopTARge24Context context)
        {
            _context = context;
        }

        //create
        public async Task<Kindergartens> Create(KindergartenDto dto)
        {
            var kg = new Kindergartens
            {
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                TeacherName = dto.TeacherName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Kindergartens.AddAsync(kg);
            await _context.SaveChangesAsync();

            return kg;
        }

        //update
        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            var kg = await _context.Kindergartens.FirstOrDefaultAsync(k => k.Id == dto.Id);
            if (kg == null) return null;

            kg.GroupName = dto.GroupName;
            kg.ChildrenCount = dto.ChildrenCount;
            kg.KindergartenName = dto.KindergartenName;
            kg.TeacherName = dto.TeacherName;
            kg.UpdatedAt = DateTime.Now;

            _context.Kindergartens.Update(kg);
            await _context.SaveChangesAsync();

            return kg;
        }

        //details
        public async Task<Kindergartens> DetailAsync(int id)
        {
            return await _context.Kindergartens.FirstOrDefaultAsync(k => k.Id == id);
        }

        //delete
        public async Task<bool> Delete(int id)
        {
            var kg = await _context.Kindergartens.FirstOrDefaultAsync(k => k.Id == id);
            if (kg == null) return false;

            _context.Kindergartens.Remove(kg);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

