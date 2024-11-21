using Microsoft.AspNetCore.Mvc;
using therapy.backend.Data;
using therapy.backend.Models.Domain;
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
    
    // GET SCHOOL BY ID
    // https://localhost:5128/api/Schools/{id}
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var schoolDomain = await schoolRepository.GetByIdAsync(id);
        
        if (schoolDomain == null)
        {
            return NotFound();
        }
        
        var schoolDto = new SchoolDto
        {
            Id = schoolDomain.Id,
            Name = schoolDomain.Name,
            Address = schoolDomain.Address
        };
        
        return Ok(schoolDto);
    }
    
    // CREATE SCHOOL
    // https://localhost:5128/api/Schools
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SchoolCreateDto schoolCreateDto)
    {
        var schoolDomain = new School
        {
            Name = schoolCreateDto.Name,
            Address = schoolCreateDto.Address
        };
        
        schoolDomain = await schoolRepository.CreateAsync(schoolDomain);
        
        var schoolDto = new SchoolDto
        {
            Id = schoolDomain.Id,
            Name = schoolDomain.Name,
            Address = schoolDomain.Address
        };
        
        return CreatedAtAction(nameof(GetById), new { id = schoolDomain.Id }, schoolDto);
    }
    
    // UPDATE SCHOOL
    // https://localhost:5128/api/Schools/{id}
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SchoolUpdateDto schoolUpdateDto)
    {
        var schoolDomain = new School
        {
            Id = id,
            Name = schoolUpdateDto.Name,
            Address = schoolUpdateDto.Address
        };
        
        schoolDomain = await schoolRepository.UpdateAsync(id, schoolDomain);
        
        if (schoolDomain == null)
        {
            return NotFound();
        }
        
        var schoolDto = new SchoolDto
        {
            Id = schoolDomain.Id,
            Name = schoolDomain.Name,
            Address = schoolDomain.Address
        };
        
        return Ok(schoolDto);
    }
    
    // DELETE SCHOOL
    // https://localhost:5128/api/Schools/{id}
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var schoolDomain = await schoolRepository.DeleteAsync(id);
        
        if (schoolDomain == null)
        {
            return NotFound();
        }
        
        var schoolDto = new SchoolDto
        {
            Id = schoolDomain.Id,
            Name = schoolDomain.Name,
            Address = schoolDomain.Address
        };
        
        return Ok(schoolDto);
    }
}