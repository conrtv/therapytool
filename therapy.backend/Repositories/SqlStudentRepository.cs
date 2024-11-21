using Microsoft.EntityFrameworkCore;
using therapy.backend.Data;
using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public class SqlStudentRepository(TherapyDbContext dbContext) : IStudentRepository
{
    public async Task<List<Student>> GetAllAsync()
    {
        return await dbContext.Students.Include(s => s.School).ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await dbContext.Students.Include(s => s.School).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
        return student;
    }

    public Task<Student?> UpdateAsync(int id, Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}