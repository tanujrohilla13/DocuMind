namespace DocuMind.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    // Set automatically by an EF Core interceptor (Day 2). Always UTC.
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
