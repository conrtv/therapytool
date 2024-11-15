using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using therapy.backend.Models.Domain;

namespace therapy.backend.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(int id, User user);
    Task<User?> DeleteAsync(int id);
}