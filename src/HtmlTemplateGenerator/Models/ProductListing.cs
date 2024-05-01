namespace HtmlTemplateGenerator.Models;

public record ProductListing
{
    public string Name { get; init; }
    public string CompanyBanner { get; init; }
    public IEnumerable<Description> Descriptions { get; init; }
    public Specification? Specification { get; init; } = null;
    public IEnumerable<Video> Videos { get; init; } = [];
    public string? ArrangementPhoto { get; init; } = null;
}