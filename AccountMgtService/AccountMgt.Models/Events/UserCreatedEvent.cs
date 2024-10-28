namespace AccountMgt.Models.Events
{
    public class UserCreatedEvent
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        // Additional properties if needed
    }
}
