using Microsoft.EntityFrameworkCore;
using therapy.backend.Models.Domain;

namespace therapy.backend.Data
{
    public class TherapyDbContext : DbContext
    {
        public TherapyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<School> Schools { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
    }
}