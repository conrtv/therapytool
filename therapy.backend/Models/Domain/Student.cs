namespace therapy.backend.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? Grade { get; set; }
        
        // Relationships
        public int SchoolId { get; set; }
        public School? School { get; set; }
        
    }
}