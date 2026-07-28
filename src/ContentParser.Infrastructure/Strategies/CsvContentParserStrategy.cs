using ContentParser.Domain.Common;
using ContentParser.Domain.Enums;
using ContentParser.Domain.Interfaces;
using ContentParser.Domain.ValueObjects;

namespace ContentParser.Infrastructure.Strategies;

public class CsvContentParserStrategy : IContentParserStrategy
{
    public ContentType SupportedType => ContentType.Csv;
    public Result<ParsedDataResult> Parse(string rawContent)
    {
        if (string.IsNullOrWhiteSpace(rawContent))
        {
            return Result<ParsedDataResult>.Failure("CSV content cannot be empty.");
        }

        var lines = rawContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length == 0)
        {
            return Result<ParsedDataResult>.Failure("CSV content does not contain any valid lines.");
        }
        
        char delimiter = lines[0].Contains(';') ? ';' : ',';

        var parsedRows = lines
            .Select(line => line.Split(delimiter).Select(cell => cell.Trim()).ToArray())
            .ToList();
        
        int processedRowsCount = parsedRows.Count > 1 ? parsedRows.Count - 1 : parsedRows.Count;

        var result = new ParsedDataResult(processedRowsCount, parsedRows);
        return Result<ParsedDataResult>.Success(result);
    }
}