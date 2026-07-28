using ContentParser.Infrastructure.Strategies;

namespace ContentParser.UnitTests.Strategies;

public class JsonContentParserStrategyTests
{
    private readonly JsonContentParserStrategy _strategy = new();

    [Fact]
    public void Parse_ShouldReturnRows_WhenJsonIsValidArray()
    {
        var json = "[{\"id\":1,\"product\":\"Laptop\"},{\"id\":2,\"product\":\"Mouse\"}]";

        var result = _strategy.Parse(json);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.ProcessedRowsCount);

        var rows = Assert.IsType<List<Dictionary<string, string>>>(result.Value.Data);
        Assert.Equal("Laptop", rows[0]["product"]);
    }

    [Fact]
    public void Parse_ShouldFail_WhenJsonIsInvalid()
    {
        var result = _strategy.Parse("{not valid json");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Parse_ShouldFail_WhenJsonArrayIsEmpty()
    {
        var result = _strategy.Parse("[]");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Parse_ShouldFail_WhenContentIsEmpty()
    {
        var result = _strategy.Parse("");

        Assert.False(result.IsSuccess);
    }
}