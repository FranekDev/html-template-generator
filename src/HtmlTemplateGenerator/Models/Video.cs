namespace HtmlTemplateGenerator.Models;

public record Video
{
    public string? Title { get; init; } = null;
    public string? Description { get; init; } = null;
    public string? Url { get; init; } = null;
}