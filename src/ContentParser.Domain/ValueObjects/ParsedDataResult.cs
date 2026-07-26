namespace ContentParser.Domain.ValueObjects;

public record ParsedDataResult(
    int ProcessedRowsCount,
    object Data
    );