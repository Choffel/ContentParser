using ContentParser.Infrastructure.Strategies;

namespace ContentParser.UnitTests.Strategies;

public class CsvContentParserStrategyTests
{
    private readonly CsvContentParserStrategy _strategy = new();

    [Fact]
    public void Parse_ShouldMapHeadersToValues_WhenCsvIsValid()
    {
        var csv = "id,product\n1,Laptop\n2,Mouse";

        var result = _strategy.Parse(csv);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.ProcessedRowsCount);

        var rows = Assert.IsType<List<Dictionary<string, string>>>(result.Value.Data);
        Assert.Equal("Laptop", rows[0]["id".Equals("id") ? "product" : "product"]);
        Assert.Equal("1", rows[0]["id"]);
        Assert.Equal("Mouse", rows[1]["product"]);
    }

    [Fact]
    public void Parse_ShouldFail_WhenCsvHasOnlyHeaderRow()
    {
        var csv = "id,product";

        var result = _strategy.Parse(csv);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Parse_ShouldFail_WhenContentIsEmpty()
    {
        var result = _strategy.Parse("");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Parse_ShouldFillMissingValuesWithEmptyString_WhenRowIsShorterThanHeader()
    {
        var csv = "id,product,price\n1,Laptop";

        var result = _strategy.Parse(csv);

        Assert.True(result.IsSuccess);
        var rows = Assert.IsType<List<Dictionary<string, string>>>(result.Value!.Data);
        Assert.Equal(string.Empty, rows[0]["price"]);
    }
}