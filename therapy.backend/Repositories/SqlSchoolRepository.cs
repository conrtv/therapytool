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
}