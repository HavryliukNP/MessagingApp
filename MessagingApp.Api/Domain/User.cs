namespace MessagingApp.Api.Domain;

public class User
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt{ get; set; } = [];
    
    public string? AvatarUrl { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}