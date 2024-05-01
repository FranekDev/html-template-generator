namespace HtmlTemplateGenerator.Models;

public record Video
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Url { get; init; }
}