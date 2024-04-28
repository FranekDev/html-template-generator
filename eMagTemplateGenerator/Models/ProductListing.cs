using System.Text.Json.Serialization;

namespace eMagTemplateGenerator.Models;

public record ProductListing
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("companyBanner")]
    public string CompanyBanner { get; init; }
    
    [JsonPropertyName("descriptions")]
    public IEnumerable<Description> Descriptions { get; init; }
    
    [JsonPropertyName("specification")]
    public Specification Specification { get; init; }
    
    [JsonPropertyName("videos")]
    public IEnumerable<Video> Videos { get; init; }
    
    [JsonPropertyName("arrangementPhoto")]
    public string? ArrangementPhoto { get; init; }
}