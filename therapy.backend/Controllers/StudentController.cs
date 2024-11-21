using Microsoft.AspNetCore.Mvc;
using therapy.backend.Data;
using therapy.backend.Models.DTO;
using therapy.backend.Models.Domain;
using therapy.backend.Repositories;

namespace therapy.backend.Controllers;

// https://localhost:5001/api/students
[ApiController]
[Route("api/students")]
public class StudentController(TherapyDbContext dbContext, IStudentRepository studentRepository) : Controller
{
    // GET ALL STUDENTS
    // https://localhost:5128/api/students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var studentsDomain = await studentRepository.GetAllAsync();
        
        var studentsDto = studentsDomain.Select(studentDomain => new StudentDto()
            {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                School = studentDomain.School != null ? new SchoolDto
                {
                    Id = studentDomain.School.Id,
                    Name = studentDomain.School.Name,
                    Address = studentDomain.School.Address
                } : null
            })
            .ToList();
        return Ok(studentsDto);
    }
    
    // GET STUDENT BY ID
    // https://localhost:5128/api/students/{id}
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var studentDomain = await studentRepository.GetByIdAsync(id);
        
        if (studentDomain == null)
        {
            return NotFound();
        }
        
        var studentDto = new StudentDto
        {
            Id = studentDomain.Id,
            FirstName = studentDomain.FirstName,
            LastName = studentDomain.LastName,
            School = studentDomain.School != null ? new SchoolDto
            {
                Id = studentDomain.School.Id,
                Name = studentDomain.School.Name,
                Address = studentDomain.School.Address
            } : null
        };
        return Ok(studentDto);
    }
    
    // CREATE STUDENT
    // https://localhost:5128/api/students
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto studentCreateDto)
    {
        var studentDomain = new Student
        {
            FirstName = studentCreateDto.FirstName,
            LastName = studentCreateDto.LastName,
            DateOfBirth = studentCreateDto.DateOfBirth,
            Grade = studentCreateDto.Grade,
            SchoolId = studentCreateDto.SchoolId
        };
        
        studentDomain = await studentRepository.CreateAsync(studentDomain);
        
        var studentDto = new StudentDto
        {
            Id = studentDomain.Id,
            FirstName = studentDomain.FirstName,
            LastName = studentDomain.LastName,
            DateOfBirth = studentDomain.DateOfBirth,
            Grade = studentDomain.Grade,
            School = studentDomain.School != null ? new SchoolDto
            {
                Id = studentDomain.School.Id,
                Name = studentDomain.School.Name,
                Address = studentDomain.School.Address
            } : null
        };
        return CreatedAtAction(nameof(GetById), new { id = studentDomain.Id }, studentDto);
    }
    
    // UPDATE STUDENT
    // https://localhost:5128/api/students/{id}
    [HttpPut]
    [Route("{id:int}")]
    
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto studentUpdateDto)
    {
        var studentDomain = new Student
        {
            FirstName = studentUpdateDto.FirstName,
            LastName = studentUpdateDto.LastName,
            DateOfBirth = studentUpdateDto.DateOfBirth,
            Grade = studentUpdateDto.Grade,
            SchoolId = studentUpdateDto.SchoolId
        };
        
        studentDomain = await studentRepository.UpdateAsync(id, studentDomain);
        
        if (studentDomain == null)
        {
            return NotFound();
        }
        
        var studentDto = new StudentDto
        {
            Id = studentDomain.Id,
            FirstName = studentDomain.FirstName,
            LastName = studentDomain.LastName,
            DateOfBirth = studentDomain.DateOfBirth,
            Grade = studentDomain.Grade,
            School = studentDomain.School != null ? new SchoolDto
            {
                Id = studentDomain.School.Id,
                Name = studentDomain.School.Name,
                Address = studentDomain.School.Address
            } : null
        };
        return Ok(studentDto);
    }
    
    // DELETE STUDENT
    // https://localhost:5128/api/students/{id}
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var studentDomain = await studentRepository.DeleteAsync(id);
        
        if (studentDomain == null)
        {
            return NotFound();
        }
        
        var studentDto = new StudentDto
        {
            Id = studentDomain.Id,
            FirstName = studentDomain.FirstName,
            LastName = studentDomain.LastName,
            DateOfBirth = studentDomain.DateOfBirth,
            Grade = studentDomain.Grade,
            School = studentDomain.School != null ? new SchoolDto
            {
                Id = studentDomain.School.Id,
                Name = studentDomain.School.Name,
                Address = studentDomain.School.Address
            } : null
        };
        return Ok(studentDto);
    }
}