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

    public async Task<Student?> UpdateAsync(int id, Student student)
    {
        var existingStudent = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        
        if (existingStudent == null)
        {
            return null;
        }
        
        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.Grade = student.Grade;
        existingStudent.SchoolId = student.SchoolId;
        
        await dbContext.SaveChangesAsync();
        return existingStudent;
    }

    public async Task<Student?> DeleteAsync(int id)
    {
        var existingStudent = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        
        if (existingStudent == null)
        {
            return null;
        }
        
        dbContext.Students.Remove(existingStudent);
        await dbContext.SaveChangesAsync();
        return existingStudent;
    }
}