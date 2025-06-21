using MessagingApp.Api.Domain;
using MessagingApp.Api.Domain.Interfaces;
using MessagingApp.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Api.Infrastructure.Repositories;

public class MessageRepository(ApplicationDbContext context) : IMessageRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _context.Messages.FindAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Message>> GetConversationMessagesAsync(Guid user1Id, Guid user2Id)
    {
        return await _context.Messages
            .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                        (m.SenderId == user2Id && m.ReceiverId == user1Id))
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<int> GetUnreadMessagesCountForUserAsync(Guid userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .CountAsync();
    }

    public async Task<IEnumerable<Message>> GetUnreadMessagesForUserAsync(Guid userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task MarkMessageAsReadAsync(Guid messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message != null)
        {
            message.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }
}