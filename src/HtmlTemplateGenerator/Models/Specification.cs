namespace HtmlTemplateGenerator.Models;

public record Specification
{
    public string? Title { get; init; } = null;
    public string? Text { get; set; } = null;
    public List<SpecificationItem> Items { get; set; } = [];
}

public record SpecificationItem
{
    public string Key { get; init; }
    public string Value { get; init; }
}