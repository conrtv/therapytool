namespace therapy.backend.Models.Domain
{
    public class UserStudent
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
