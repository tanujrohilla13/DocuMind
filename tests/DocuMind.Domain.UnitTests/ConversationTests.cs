using DocuMind.Domain.Entities;
using DocuMind.Domain.Enums;
using DocuMind.Domain.Exceptions;

namespace DocuMind.Domain.UnitTests;

public class ConversationTests
{
    [Fact]
    public void AddMessage_WithValidContent_AddsMessageAndUpdatesTimestamp()
    {
        var conversation = Conversation.Create(Guid.NewGuid(), "My chat");

        var message = conversation.AddMessage(MessageRole.User, "Hello", 2);

        Assert.Single(conversation.Messages);
        Assert.Equal(conversation.Id, message.ConversationId);
        Assert.NotNull(conversation.UpdatedAtUtc);
    }

    [Fact]
    public void AddMessage_WithEmptyContent_ThrowsDomainException()
    {
        var conversation = Conversation.Create(Guid.NewGuid(), "My chat");

        Assert.Throws<DomainException>(() => conversation.AddMessage(MessageRole.User, " ", 0));
    }
}
