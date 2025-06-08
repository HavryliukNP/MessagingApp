using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Api.Domain;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }

    [MaxLength(500)] public string Content { get; set; } = string.Empty;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;

    public User Sender { get; set; } = null!;
    public User Receiver { get; set; } = null!;
}