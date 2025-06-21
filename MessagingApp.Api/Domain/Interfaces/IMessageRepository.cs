namespace MessagingApp.Api.Domain.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(Guid id);

    Task<IEnumerable<Message>> GetConversationMessagesAsync(Guid user1Id, Guid user2Id);

    Task<int> GetUnreadMessagesCountForUserAsync(Guid userId);
    Task<IEnumerable<Message>> GetUnreadMessagesForUserAsync(Guid userId);

    Task MarkMessageAsReadAsync(Guid messageId);
}