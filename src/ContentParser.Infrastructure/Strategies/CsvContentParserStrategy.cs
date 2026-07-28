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

        if (lines.Length < 2)
        {
            return Result<ParsedDataResult>.Failure("CSV must contain a header row and at least one data row.");
        }

        var headers = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        var rows = new List<Dictionary<string, string>>();

        for (var i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',').Select(v => v.Trim()).ToArray();
            var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (var j = 0; j < headers.Length; j++)
            {
                row[headers[j]] = j < values.Length ? values[j] : string.Empty;
            }

            rows.Add(row);
        }

        return Result<ParsedDataResult>.Success(new ParsedDataResult(rows.Count, rows));
    }
}