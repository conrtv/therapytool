namespace therapy.backend.Models.DTO;

public class SchoolDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    // Relationships
    public ICollection<StudentDto> Students { get; set; }
    public ICollection<UserDto> Users { get; set; }
}