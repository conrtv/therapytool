using Microsoft.EntityFrameworkCore;
using therapy.backend.Data;
using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public class SqlSchoolRepository(TherapyDbContext dbContext) : ISchoolRepository
{
    public async Task<List<School>> GetAllAsync()
    {
        return await dbContext.Schools.ToListAsync();
    }

    public async Task<School?> GetByIdAsync(int id)
    {
        return await dbContext.Schools.FirstOrDefaultAsync(school => school.Id == id);
    }

    public async Task<School> CreateAsync(School school)
    {
        await dbContext.Schools.AddAsync(school);
        await dbContext.SaveChangesAsync();
        return school;
    }
    
    public async Task<School?> UpdateAsync(int id, School school)
    {
        var existingSchool = await dbContext.Schools.FirstOrDefaultAsync(s => s.Id == id);
        
        if (existingSchool == null)
        {
            return null;
        }
        
        existingSchool.Name = school.Name;
        existingSchool.Address = school.Address;
        
        await dbContext.SaveChangesAsync();
        return existingSchool;
    }

    public async Task<School?> DeleteAsync(int id)
    {
        var existingSchool = await dbContext.Schools.FirstOrDefaultAsync(s => s.Id == id);
        
        if (existingSchool == null)
        {
            return null;
        }
        
        dbContext.Schools.Remove(existingSchool);
        await dbContext.SaveChangesAsync();
        return existingSchool;
    }
}