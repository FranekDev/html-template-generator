namespace HtmlTemplateGenerator.Models;

public record Description
{
    public string Title { get; init; }
    public string Text { get; init; }
    public string? ImageSrc { get; init; } = null;
}