namespace therapy.backend.Models.DTO;

public class UserDto
{
    public int Id { get; set; } //Unique Identifier
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } //Role (Admin, OT, PT, OTA, PTA)
    
    // Relationships
    public ICollection<SchoolDto> Schools { get; set; }
}