namespace therapy.backend.Models.Domain
{
    public class User
    {
        public int Id { get; set; } //Unique Identifier
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } //Role (Admin, OT, PT, OTA, PTA)

        // Relationships
        public ICollection<School> Schools { get; set; } = new List<School>();
    }
}