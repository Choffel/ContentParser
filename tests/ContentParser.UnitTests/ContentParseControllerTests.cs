using System.Net.Mime;
using System.Text.Json;
using ContentParser.Api.Controller;
using ContentParser.Application.DTOs;
using ContentParser.Application.Interfaces;
using ContentParser.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ContentType = ContentParser.Domain.Enums.ContentType;

namespace ContentParser.UnitTests.Services;

public class ContentParseControllerTests
{
     private readonly Mock<IContentParseService> _serviceMock = new();
    private readonly ContentParseController _controller;

    public ContentParseControllerTests()
    {
        _controller = new ContentParseController(_serviceMock.Object);
    }

    [Fact]
    public void ParseContent_ShouldReturnOk_WhenServiceSucceeds()
    {
        // Arrange
        var request = new ParseContentRequestDto("someBase64", ContentType.Csv);
        var response = new ParseContentResponseDto(1, new object());

        _serviceMock
            .Setup(s => s.ProcessPayload(request))
            .Returns(Result<ParseContentResponseDto>.Success(response));

        // Act
        var result = _controller.ParseContent(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public void ParseContent_ShouldReturnBadRequest_WhenServiceFails()
    {
        // Arrange
        var request = new ParseContentRequestDto("", ContentType.Csv);

        _serviceMock
            .Setup(s => s.ProcessPayload(request))
            .Returns(Result<ParseContentResponseDto>.Failure("Content cannot be null or empty."));

        // Act
        var result = _controller.ParseContent(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Theory]
    [InlineData("CSV", ContentType.Csv)]
    [InlineData("INTERNAL_JSON", ContentType.InternalJson)]
    public void RequestDto_ShouldDeserializeTypeCorrectly_FromRawJson(string typeValue, ContentType expected)
    {
        var json = $"{{\"type\":\"{typeValue}\",\"content\":\"dGVzdA==\"}}";

        var dto = JsonSerializer.Deserialize<ParseContentRequestDto>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(dto);
        Assert.Equal(expected, dto.Type);
    }
}