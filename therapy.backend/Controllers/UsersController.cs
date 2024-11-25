﻿using Microsoft.AspNetCore.Mvc;
using therapy.backend.Data;
using therapy.backend.Models.Domain;
using therapy.backend.Models.DTO;
using therapy.backend.Repositories;

namespace therapy.backend.Controllers;
// https://localhost:5128/api/Users
[Route("api/[controller]")]
[ApiController]
public class UsersController(TherapyDbContext dbContext, IUserRepository userRepository, IStudentRepository studentRepository) : Controller
{
    // GET ALL USERS
    // https://localhost:5128/api/Users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usersDomain = await userRepository.GetAllAsync();
        
        var usersDto = usersDomain.Select(userDomain => new UserDto()
            {
                Id = userDomain.Id,
                FirstName = userDomain.FirstName,
                LastName = userDomain.LastName,
                Email = userDomain.Email,
                Role = userDomain.Role,
                Schools = userDomain.Schools?.Select(s => new SchoolDto { Id = s.Id, Name = s.Name, Address = s.Address }).ToList() ?? new List<SchoolDto>()
            })
            .ToList();

        return Ok(usersDto);
    }
    
    // GET USER BY ID
    // https://localhost:5128/api/Users/{id}
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userDomain = await userRepository.GetByIdAsync(id);
        
        if (userDomain == null)
        {
            return NotFound();
        }
        
        var userDto = new UserDto
        {
            Id = userDomain.Id,
            FirstName = userDomain.FirstName,
            LastName = userDomain.LastName,
            Email = userDomain.Email,
            Role = userDomain.Role,
            Schools = userDomain.Schools?.Select(s => new SchoolDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address
            }).ToList() ?? [] 
        };
        
        return Ok(userDto);
    }
    
    // CREATE USER
    // https://localhost:5128/api/Users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.PasswordHash);
        
        var userDomain = new User
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email,
            PasswordHash = hashedPassword,
            Role = userCreateDto.Role
        };
        
        userDomain = await userRepository.CreateAsync(userDomain);

        var userDto = new UserDto()
        {
            Id = userDomain.Id,
            FirstName = userDomain.FirstName,
            LastName = userDomain.LastName,
            Email = userDomain.Email,
            Role = userDomain.Role
        };

        return CreatedAtAction(nameof(GetById), new { id = userDomain.Id }, userDto);
    }
    
    // UPDATE USER
    // https://localhost:5128/api/Users/{id}
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userUpdateDto)
    {
        // map UserUpdateDto to User domain model
        var userDomain = new User
        {
            FirstName = userUpdateDto.FirstName,
            LastName = userUpdateDto.LastName,
            Email = userUpdateDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.PasswordHash),
            Role = userUpdateDto.Role
        };
        
        userDomain = await userRepository.UpdateAsync(id, userDomain);
        
        if (userDomain == null)
        {
            return NotFound();
        }
        var userDto = new UserDto
        {
            Id = userDomain.Id,
            FirstName = userDomain.FirstName,
            LastName = userDomain.LastName,
            Email = userDomain.Email,
            Role = userDomain.Role
        };
        
        return Ok(userDto);
    }
    
    // DELETE USER
    // https://localhost:5128/api/Users/{id}
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userDomain = await userRepository.DeleteAsync(id);
        
        if (userDomain == null)
        {
            return NotFound();
        }
        
        // return deleted User back
        // map User to UserDto
        var userDto = new UserDto
        {
            Id = userDomain.Id,
            FirstName = userDomain.FirstName,
            LastName = userDomain.LastName,
            Email = userDomain.Email,
            Role = userDomain.Role
        };
        return Ok(userDto);
    }
    
    // ASSIGN STUDENT TO USER
    // https://localhost:5128/api/Users/{userId}/students/{studentId}
    [HttpPost]
    [Route("{userId:int}/students/assign")]
    public async Task<IActionResult> AssignStudentsToUser(int userId, [FromBody] StudentAssignDto studentAssignDto)
    {
        
        
        var user = await userRepository.GetUserWithStudentsAsync(userId);
        
        if (user == null) return NotFound();

        var students = await studentRepository.GetStudentsByIdAsync(StudentAssignDto.StudentIds);
        
        Console.WriteLine("Retrieved Student IDs: " + string.Join(", ", students.Select(s => s.Id)));
        
        if (!students.Any()) return BadRequest("No valid students provided.");
        
        user.UserStudents = students.Select(s => new UserStudent
        {
            UserId = userId,
            StudentId = s.Id
        }).ToList();

        await userRepository.UpdateAsync(user.Id, user);
        
        return Ok("Students assigned successfully.");

    }
    
}