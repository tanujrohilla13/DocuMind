using DocuMind.Domain.Common;
using DocuMind.Domain.Exceptions;

namespace DocuMind.Domain.Entities;

public sealed class DocumentChunk : BaseEntity
{
    private DocumentChunk() { } // for EF Core only

    public Guid DocumentId { get; private set; }
    public int ChunkIndex { get; private set; }
    public string Content { get; private set; } = default!;
    public int? PageNumber { get; private set; }   // null for formats without pages (e.g. DOCX)
    public int TokenCount { get; private set; }

    public static DocumentChunk Create(Guid documentId, int chunkIndex, string content,
                                       int? pageNumber, int tokenCount)
    {
        if (documentId == Guid.Empty) throw new DomainException("Document is required.");
        if (chunkIndex < 0) throw new DomainException("Chunk index cannot be negative.");
        if (string.IsNullOrWhiteSpace(content)) throw new DomainException("Chunk content is required.");
        if (pageNumber is <= 0) throw new DomainException("Page number must be positive.");
        if (tokenCount <= 0) throw new DomainException("Token count must be positive.");

        return new DocumentChunk
        {
            DocumentId = documentId,
            ChunkIndex = chunkIndex,
            Content = content,
            PageNumber = pageNumber,
            TokenCount = tokenCount
        };
    }
}
