using DocuMind.Domain.Common;
using DocuMind.Domain.Enums;
using DocuMind.Domain.Exceptions;

namespace DocuMind.Domain.Entities;

public sealed class Document : AuditableEntity
{
    private Document() { } // for EF Core only

    public Guid OwnerId { get; private set; }
    public string FileName { get; private set; } = default!;
    public string ContentType { get; private set; } = default!;
    public long SizeBytes { get; private set; }
    public string StoragePath { get; private set; } = default!;
    public DocumentStatus Status { get; private set; }
    public string? FailureReason { get; private set; }

    public static Document Create(Guid ownerId, string fileName, string contentType,
                                  long sizeBytes, string storagePath)
    {
        if (ownerId == Guid.Empty) throw new DomainException("Owner is required.");
        if (string.IsNullOrWhiteSpace(fileName)) throw new DomainException("File name is required.");
        if (sizeBytes <= 0) throw new DomainException("File cannot be empty.");

        return new Document
        {
            OwnerId = ownerId,
            FileName = fileName.Trim(),
            ContentType = contentType,
            SizeBytes = sizeBytes,
            StoragePath = storagePath,
            Status = DocumentStatus.Uploaded
        };
    }

    public void MarkProcessing()
    {
        if (Status != DocumentStatus.Uploaded)
            throw new DomainException($"Cannot process a document in status {Status}.");
        Status = DocumentStatus.Processing;
    }

    public void MarkReady() => Status = DocumentStatus.Ready;

    public void MarkFailed(string reason)
    {
        Status = DocumentStatus.Failed;
        FailureReason = reason;
    }
}
