namespace HtmlTemplateGenerator.Models;

public record Description
{
    public string? Title { get; init; } = null;
    public string? Text { get; init; } = null;
    public string? ImageSrc { get; init; } = null;
}