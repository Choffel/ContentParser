using ContentParser.Domain.Enums;

namespace ContentParser.Application.DTOs;

public record ParseContentRequestDto(
    string Content,
    ContentType Type
    );