using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Services;
using HtmlTemplateGenerator.Services.Interfaces;

namespace HtmlTemplateGenerator.Validation;

public class ItemValidator
{
    private readonly ILoggerService _logger = new LoggerService();

    public bool ValidateItem(Item? item)
    {
        if (item is not null)
        {
            return true;
        }

        _logger.LogError("Failed to deserialize Yaml data.");
        return false;
    }

    public bool ValidateItemName(string? name)
    {
        if (name is not null)
        {
            return true;
        }

        _logger.LogError("Item name is required.");
        return false;
    }

    public bool ShouldRenderBannerImage(string? bannerImageSrc)
    {
        if (bannerImageSrc is not null)
        {
            return true;
        }

        _logger.LogInformation("No banner image found.");
        return false;
    }

    public bool ShouldRenderHeader(string? header)
    {
        if (header is not null)
        {
            return true;
        }

        _logger.LogInformation("No header found.");
        return false;
    }

    public bool ShouldRenderDescriptions(IEnumerable<Description>? descriptions)
    {
        if (descriptions?.Any() is true)
        {
            return true;
        }

        _logger.LogInformation("No description items found.");
        return false;
    }

    public bool ShouldRenderSpecification(Specification? specification)
    {
        if (specification is not null && specification.Items.Count > 0)
        {
            return true;
        }

        _logger.LogInformation("No specification items found.");
        return false;
    }

    public bool ShouldRenderVideos(IEnumerable<Video>? videos)
    {
        if (videos?.Any() is true)
        {
            return true;
        }

        _logger.LogInformation("No videos found.");
        return false;
    }
}