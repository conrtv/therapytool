using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public interface ISchoolRepository
{
    Task<List<School>> GetAllAsync();
    Task<School?> GetByIdAsync(int id);
    Task<School> CreateAsync(School school);
    Task<School?> UpdateAsync(int id, School school);
    Task<School?> DeleteAsync(int id);
}