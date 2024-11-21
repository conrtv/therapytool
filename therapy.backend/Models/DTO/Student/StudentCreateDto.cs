namespace therapy.backend.Models.DTO;

public class StudentCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? Grade { get; set; }
    
    // Relationships
    public int SchoolId { get; set; }
}