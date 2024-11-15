using Microsoft.AspNetCore.Mvc;
using therapy.backend.Data;
using therapy.backend.Models.DTO;
using therapy.backend.Repositories;

namespace therapy.backend.Controllers;
// https://localhost:5128/api/Schools
[Route("api/[controller]")]
[ApiController]
public class SchoolController(TherapyDbContext dbContext, ISchoolRepository schoolRepository) : Controller
{
    // GET ALL SCHOOLS
    // https://localhost:5128/api/Schools
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var schoolsDomain = await schoolRepository.GetAllAsync();
        
        var schoolsDto = schoolsDomain.Select(schoolDomain => new SchoolDto
            {
                Id = schoolDomain.Id,
                Name = schoolDomain.Name,
                Address = schoolDomain.Address
            })
            .ToList();
        
        return Ok(schoolsDto);
    }
}