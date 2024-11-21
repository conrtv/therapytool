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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Student>()
                .HasOne(s => s.School)
                .WithMany(sch => sch.Students)
                .HasForeignKey(s => s.SchoolId)
                .IsRequired();
            
            // Configure UserStudent relationship
            modelBuilder.Entity<UserStudent>()
                .HasKey(us => new { us.UserId, us.StudentId });

            modelBuilder.Entity<UserStudent>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserStudents)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserStudent>()
                .HasOne(us => us.Student)
                .WithMany(s => s.UserStudents)
                .HasForeignKey(us => us.StudentId);
            
            // Seed data for Users
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "email@email.com",
                    PasswordHash = "password",
                    Role = "Admin"
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "email@email.com",
                    PasswordHash = "password",
                    Role = "OT"
                },
                new User()
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "email@email.com",
                    PasswordHash = "password",
                    Role = "PT"
                }
            };
            //seed users to the database
            modelBuilder.Entity<User>().HasData(users);
            
            
            //Seed data for Schools
            var schools = new List<School>()
            {
                new School()
                {
                    Id = 1,
                    Name = "School 1",
                    Address = "1234 School St"
                },
                new School()
                {
                    Id = 2,
                    Name = "School 2",
                    Address = "5678 School St"
                },
                new School()
                {
                    Id = 3,
                    Name = "School 3",
                    Address = "91011 School St"
                }
            };
            //seed schools to the database
            modelBuilder.Entity<School>().HasData(schools);
            
            //Seed data for Students
            var students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "Student",
                    LastName = "One",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    SchoolId = 1
                },
                new Student()
                {
                    Id = 2,
                    FirstName = "Student",
                    LastName = "Two",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    SchoolId = 1
                },
                new Student()
                {
                    Id = 3,
                    FirstName = "Student",
                    LastName = "Three",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    SchoolId = 3
                }
            };
            
            //seed students to the database
            modelBuilder.Entity<Student>().HasData(students);
            

        }
    }
}