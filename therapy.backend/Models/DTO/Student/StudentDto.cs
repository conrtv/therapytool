namespace therapy.backend.Models.DTO;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? Grade { get; set; }
    
    // Relationships
    public int SchoolId { get; set; }
    public SchoolDto? School { get; set; }
}