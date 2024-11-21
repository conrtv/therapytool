using Microsoft.EntityFrameworkCore;
using therapy.backend.Data;
using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public class SqlUserRepository(TherapyDbContext dbContext) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await dbContext.Users.Include(user => user.Schools).FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<User?> GetUserWithStudentsAsync(int id)
    {
        return await dbContext.Users
            .Include(u => u.UserStudents)
            .ThenInclude(us => us.Student) // Load the assigned students
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<User> CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User user)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if (existingUser == null)
        {
            return null;
        }
        
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.Role = user.Role;
        existingUser.Schools = user.Schools;
        
        await dbContext.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if (existingUser == null)
        {
            return null;
        }
        
        dbContext.Users.Remove(existingUser);
        await dbContext.SaveChangesAsync();
        return existingUser;
    }
}