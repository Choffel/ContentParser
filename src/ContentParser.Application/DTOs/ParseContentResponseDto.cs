namespace ContentParser.Application.DTOs;

public record ParseContentResponseDto(
    int ProcessedRowsCount,
    object Data
    );