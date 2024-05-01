namespace HtmlTemplateGenerator.Models;

public record Item
{
    public string Name { get; init; }
    public string? BannerImageSrc { get; init; } = null;
    public string? Header { get; init; } = null;
    public IEnumerable<Description> Descriptions { get; init; }
    public Specification? Specification { get; init; } = null;
    public IEnumerable<Video> Videos { get; init; } = [];
}