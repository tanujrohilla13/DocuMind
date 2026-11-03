using DocuMind.Domain.Common;
using DocuMind.Domain.Enums;
using DocuMind.Domain.Exceptions;

namespace DocuMind.Domain.Entities;

public sealed class Conversation : AuditableEntity
{
    private readonly List<Message> _messages = [];

    private Conversation() { } // for EF Core only

    public Guid OwnerId { get; private set; }
    public string Title { get; private set; } = default!;

    // Read-only view: callers cannot Add/Remove/Clear and bypass AddMessage rules.
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public static Conversation Create(Guid ownerId, string title)
    {
        if (ownerId == Guid.Empty) throw new DomainException("Owner is required.");
        if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Title is required.");

        return new Conversation { OwnerId = ownerId, Title = title.Trim() };
    }

    public Message AddMessage(MessageRole role, string content, int tokenCount)
    {
        if (!Enum.IsDefined(role)) throw new DomainException("Invalid message role.");
        if (string.IsNullOrWhiteSpace(content)) throw new DomainException("Message content is required.");
        if (tokenCount < 0) throw new DomainException("Token count cannot be negative.");

        var message = Message.Create(Id, role, content, tokenCount);
        _messages.Add(message);
        UpdatedAtUtc = DateTime.UtcNow;
        return message;
    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new DomainException("Title is required.");
        Title = title.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
