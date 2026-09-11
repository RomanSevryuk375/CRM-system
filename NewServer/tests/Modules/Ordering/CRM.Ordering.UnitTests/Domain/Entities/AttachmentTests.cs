using CRM.TestHelpers;
using CRM.Ordering.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class AttachmentTests
{
    private static readonly OrderId TestOrderId = new(Guid.NewGuid());
    private static readonly WorkerId TestWorkerId = new(Guid.NewGuid());

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new AttachmentId(Guid.NewGuid());

        // Act
        var result = Attachment.Create(
            id,
            TestOrderId,
            TestWorkerId,
            "invoice.pdf",
            "/uploads/invoice.pdf",
            "application/pdf",
            1024 * 50,
            "Invoice attachment");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.OrderId.Should().Be(TestOrderId);
        result.Value.UploadedBy.Should().Be(TestWorkerId);
        result.Value.FileName.Should().Be("invoice.pdf");
        result.Value.FilePath.Should().Be("/uploads/invoice.pdf");
        result.Value.ContentType.Should().Be("application/pdf");
        result.Value.FileSize.Should().Be(1024 * 50);
        result.Value.Description.Should().Be("Invoice attachment");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(Attachment.MaxFileSize + 1)]
    public void Create_WithInvalidFileSize_ReturnsFailure(long fileSize)
    {
        // Act
        var result = Attachment.Create(
            new AttachmentId(Guid.NewGuid()),
            TestOrderId,
            TestWorkerId,
            "doc.pdf",
            "/path/doc.pdf",
            "application/pdf",
            fileSize,
            null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Attachment.Errors.InvalidFileSize);
    }

    [Theory]
    [InlineData("text/plain")]
    [InlineData("application/zip")]
    [InlineData("video/mp4")]
    public void Create_WithUnsupportedContentType_ReturnsFailure(string contentType)
    {
        // Act
        var result = Attachment.Create(
            new AttachmentId(Guid.NewGuid()),
            TestOrderId,
            TestWorkerId,
            "file.txt",
            "/path/file.txt",
            contentType,
            1024,
            null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Attachment.Errors.UnsupportedContentType);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyFileName_ReturnsFailure(string? fileName)
    {
        // Act
        var result = Attachment.Create(
            new AttachmentId(Guid.NewGuid()),
            TestOrderId,
            TestWorkerId,
            fileName!,
            "/path/file.pdf",
            "application/pdf",
            1024,
            null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Attachment.Errors.FileNameRequired);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyFilePath_ReturnsFailure(string? filePath)
    {
        // Act
        var result = Attachment.Create(
            new AttachmentId(Guid.NewGuid()),
            TestOrderId,
            TestWorkerId,
            "file.pdf",
            filePath!,
            "application/pdf",
            1024,
            null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Attachment.Errors.FilePathRequired);
    }
}

