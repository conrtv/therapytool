using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<List<Student>> GetBySchoolIdAsync(int schoolId);
    Task<Student> CreateAsync(Student student);
    Task<Student?> UpdateAsync(int id, Student student);
    Task<Student?> DeleteAsync(int id);
}