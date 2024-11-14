namespace therapy.backend.Models.Domain
{

    public class School
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        
        // Relationships
        public required ICollection<Student> Students { get; set; }
        public required ICollection<User> Users { get; set; }
    }
}