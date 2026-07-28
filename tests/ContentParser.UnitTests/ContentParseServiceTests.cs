using ContentParser.Application.DTOs;
using ContentParser.Application.Interfaces;
using ContentParser.Application.Services;
using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;
using ContentParser.Domain.ValueObjects;
using Moq;
using Xunit;

namespace ContentParser.UnitTests.Services;

public class ContentParseServiceTests
{
    private readonly Mock<IContentParserResolver> _resolverMock = new();
    private readonly Mock<IBase64Decoder> _decoderMock = new();
    private readonly Mock<IContentParserStrategy> _strategyMock = new();
    
    private readonly ContentParseService _service;

    public ContentParseServiceTests()
    {
        _service = new ContentParseService(_resolverMock.Object, _decoderMock.Object);
    }

    [Fact]
    public void ProcessPayload_ShouldReturnFailure_WhenRequestIsInvalid()
    {
        // Arrange
        var invalidRequest = new ParseContentRequestDto(
            Content: "", 
            Type: ContentType.Csv
        );

        // Act
        var result = _service.ProcessPayload(invalidRequest);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.ErrorMessage);
        Assert.Contains("Content cannot be null or empty", result.ErrorMessage);
        
        _decoderMock.Verify(d => d.Decode(It.IsAny<string>()), Times.Never);
        _resolverMock.Verify(r => r.Resolve(It.IsAny<ContentType>()), Times.Never);
    }

    [Fact]
    public void ProcessPayload_ShouldSuccessfullyParseInternalJson_WhenRequestIsValid()
    {
        // Arrange
        var rawBase64 = "W3siaWQiOjF9XQ==";
        var decodedJson = "[{\"id\":1}]";
        var request = new ParseContentRequestDto(rawBase64, ContentType.InternalJson);

        _resolverMock
            .Setup(r => r.Resolve(ContentType.InternalJson))
            .Returns(Result<IContentParserStrategy>.Success(_strategyMock.Object));

        _decoderMock
            .Setup(d => d.Decode(rawBase64))
            .Returns(Result<string>.Success(decodedJson));

        var expectedParsedData = new ParsedDataResult(
            ProcessedRowsCount: 1, 
            Data: new[] { new { id = 1 } }
        );

        _strategyMock
            .Setup(s => s.Parse(decodedJson))
            .Returns(Result<ParsedDataResult>.Success(expectedParsedData));

        // Act
        var result = _service.ProcessPayload(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.ProcessedRowsCount);
        
        _resolverMock.Verify(r => r.Resolve(ContentType.InternalJson), Times.Once);
        _decoderMock.Verify(d => d.Decode(rawBase64), Times.Once);
        _strategyMock.Verify(s => s.Parse(decodedJson), Times.Once);
    }

    [Fact]
    public void ProcessPayload_ShouldSuccessfullyParseCsv_WhenRequestIsValid()
    {
        // Arrange
        var rawBase64 = "aWQscHJvZHVjdAoxLExhcHRvcA==";
        var decodedCsv = "id,product\n1,Laptop";
        var request = new ParseContentRequestDto(rawBase64, ContentType.Csv);

        _resolverMock
            .Setup(r => r.Resolve(ContentType.Csv))
            .Returns(Result<IContentParserStrategy>.Success(_strategyMock.Object));

        _decoderMock
            .Setup(d => d.Decode(rawBase64))
            .Returns(Result<string>.Success(decodedCsv));

        var expectedCsvData = new ParsedDataResult(
            ProcessedRowsCount: 1, 
            Data: new[] 
            { 
                new[] { "id", "product" }, 
                new[] { "1", "Laptop" } 
            }
        );

        _strategyMock
            .Setup(s => s.Parse(decodedCsv))
            .Returns(Result<ParsedDataResult>.Success(expectedCsvData));

        // Act
        var result = _service.ProcessPayload(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.ProcessedRowsCount);

        _resolverMock.Verify(r => r.Resolve(ContentType.Csv), Times.Once);
        _decoderMock.Verify(d => d.Decode(rawBase64), Times.Once);
        _strategyMock.Verify(s => s.Parse(decodedCsv), Times.Once);
    }
}