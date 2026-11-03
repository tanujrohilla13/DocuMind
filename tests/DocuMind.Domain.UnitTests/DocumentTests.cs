using DocuMind.Domain.Entities;
using DocuMind.Domain.Enums;
using DocuMind.Domain.Exceptions;

namespace DocuMind.Domain.UnitTests;

public class DocumentTests
{
    private static readonly Guid OwnerId = Guid.NewGuid();

    private static Document CreateValidDocument() =>
        Document.Create(OwnerId, "  report.pdf ", "application/pdf", 1024, "uploads/abc.pdf");

    [Fact]
    public void Create_WithValidData_SetsStatusUploaded()
    {
        var document = CreateValidDocument();

        Assert.Equal(DocumentStatus.Uploaded, document.Status);
        Assert.Equal(OwnerId, document.OwnerId);
        Assert.Equal("report.pdf", document.FileName); // trimmed
        Assert.NotEqual(Guid.Empty, document.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyFileName_ThrowsDomainException(string fileName)
    {
        var ex = Assert.Throws<DomainException>(() =>
            Document.Create(OwnerId, fileName, "application/pdf", 1024, "uploads/abc.pdf"));

        Assert.Equal("File name is required.", ex.Message);
    }

    [Fact]
    public void MarkProcessing_WhenReady_ThrowsDomainException()
    {
        var document = CreateValidDocument();
        document.MarkProcessing();
        document.MarkReady();

        Assert.Throws<DomainException>(document.MarkProcessing);
    }
}
