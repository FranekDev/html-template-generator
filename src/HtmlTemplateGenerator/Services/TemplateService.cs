using HtmlTemplateGenerator.Builder;
using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Static;

namespace HtmlTemplateGenerator.Services;

public class TemplateService
{
    private readonly FileManagerService _fileManager = new();
    private readonly LoggerService _logger = new();
    private readonly TemplateBuilder _templateBuilder = new();
    
    private Item? GetItem(string data)
    {
        var item = _fileManager.DeserializeYamlFile<Item>(data);

        if (item?.Specification?.Text is null)
        {
            return item;
        }
        
        try
        {
            item.Specification.Items.GenerateSpecificationItems(item.Specification.Text);
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to generate specification items: {e.Message}");
            return null;
        }

        return item;
    }

    private async Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Templates")
    {
        await _fileManager.WriteAndSaveHtmlFileToDirectory(fileName, htmlPage, directoryName);
    }
    
    public async Task GenerateHtmlTemplateFile(string fileName)
    {
        if (!_fileManager.CheckIfFileExists(fileName))
        {
            _logger.LogError($"File {fileName} not found.");
            return;
        }
        
        var data = await _fileManager.ReadFile(fileName);
        
        if (string.IsNullOrEmpty(data))
        {
            _logger.LogError("Failed to read Yaml file.");
            return;
        }
        
        var item = GetItem(data);
        if (item is null)
        {
            _logger.LogError("Failed to deserialize Yaml data.");
            return;
        }

        if (item.Name is null)
        {
            _logger.LogError("Item name is required.");
            return;
        }
        
        var shouldRenderBannerImage = ValidateBannerImage(item.BannerImageSrc);
        var shouldRenderHeader = ValidateHeader(item.Header);
        var shouldRenderDescriptions = ValidateDescriptions(item.Descriptions);
        var shouldRenderSpecification = ValidateSpecification(item.Specification);
        var shouldRenderVideos = ValidateVideos(item.Videos);
        
        _logger.LogInformation("Generating template file...");
        
        var htmlTemplate = _templateBuilder.GenerateHtmlTemplate(
            item, 
            shouldRenderBannerImage,
            shouldRenderHeader,
            shouldRenderDescriptions,
            shouldRenderSpecification, 
            shouldRenderVideos
        );
        
        await SaveHtmlFileToDirectory(item.Name, htmlTemplate);
        _logger.LogSuccess($"Template file for {item.Name} generated successfully.");
        
    }
    
    private bool ValidateBannerImage(string? bannerImageSrc)
    {
        if (bannerImageSrc is not null)
        {
            return true;
        }
        
        _logger.LogInformation("No banner image found.");
        return false;
    }

    private bool ValidateHeader(string? header)
    {
        if (header is not null)
        {
            return true;
        }
        
        _logger.LogInformation("No header found.");
        return false;
    }

    private bool ValidateDescriptions(IEnumerable<Description> descriptions)
    {
        if (descriptions?.Any() is true)
        {
            return true;
        }
        
        _logger.LogInformation("No description items found.");
        return false;
    }
 
    private bool ValidateSpecification(Specification? specification)
    {
        if (specification is not null && specification.Items.Count > 0)
        {
            return true;
        }
        
        _logger.LogInformation("No specification items found.");
        return false;
    }

    private bool ValidateVideos(IEnumerable<Video> videos)
    {
        if (videos?.Any() is true)
        {
            return true;
        }
        
        _logger.LogInformation("No videos found.");
        return false;
    }
}