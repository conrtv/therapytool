namespace therapy.backend.Models.Domain
{

    public class School
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        
        // Relationships
        public ICollection<Student>? Students { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}