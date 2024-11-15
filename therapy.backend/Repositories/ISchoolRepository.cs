using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public interface ISchoolRepository
{
    Task<List<School>> GetAllAsync();
}