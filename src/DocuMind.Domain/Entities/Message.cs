using DocuMind.Domain.Common;
using DocuMind.Domain.Enums;

namespace DocuMind.Domain.Entities;

public sealed class Message : AuditableEntity
{
    private Message() { } // for EF Core only

    public Guid ConversationId { get; private set; }
    public MessageRole Role { get; private set; }
    public string Content { get; private set; } = default!;
    public int TokenCount { get; private set; }

    // internal: only Conversation (the aggregate root) can create messages.
    internal static Message Create(Guid conversationId, MessageRole role, string content, int tokenCount) =>
        new()
        {
            ConversationId = conversationId,
            Role = role,
            Content = content,
            TokenCount = tokenCount,
            CreatedAtUtc = DateTime.UtcNow
        };
}
