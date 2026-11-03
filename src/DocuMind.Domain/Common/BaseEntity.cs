namespace DocuMind.Domain.Common;

public abstract class BaseEntity
{
    // Version 7 GUIDs are time-ordered, so SQL Server clustered indexes don't fragment.
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
}
