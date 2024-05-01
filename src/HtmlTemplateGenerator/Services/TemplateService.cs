using HtmlTemplateGenerator.Builder;
using HtmlTemplateGenerator.Models;

namespace HtmlTemplateGenerator.Services;

public class TemplateService
{
    private readonly FileManagerService _fileManager = new();
    private readonly LoggerService _logger = new();
    private readonly TemplateBuilder _templateBuilder = new();
    
    private Item? GetItem(string data)
    {
        var item = _fileManager.DeserializeYamlFile<Item>(data);

        if (item?.Specification?.Text != null)
        {
            item.Specification.Items =
                item.Specification.GenerateSpecificationItems(item.Specification.Text);
        }

        return item;
    }

    private async Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Templates")
    {
        await _fileManager.WriteAndSaveHtmlFileToDirectory(fileName, htmlPage, directoryName);
    }
    
    public async Task GenerateHtmlTemplateFile(string fileName)
    {
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
        
        var shouldRenderBannerImage = item.BannerImageSrc is not null;
        var shouldRenderHeader = item.Header is not null;
        var shouldRenderSpecification = item.Specification is not null && item.Specification.Items.Any();
        var shouldRenderVideos = item.Videos.Any();
        
        _logger.LogInformation("Generating template file...");
        
        if (!shouldRenderBannerImage)
        {
            _logger.LogInformation("No banner image found.");
        }
        
        if (!shouldRenderHeader)
        {
            _logger.LogInformation("No header found.");
        }
        
        if (!shouldRenderSpecification)
        {
            _logger.LogInformation("No specification items found.");
        }
        
        if (!shouldRenderVideos)
        {
            _logger.LogInformation("No videos found.");
        }
        
        var htmlTemplate = _templateBuilder.GenerateHtmlTemplate(
            item, 
            shouldRenderBannerImage,
            shouldRenderHeader,
            shouldRenderSpecification, 
            shouldRenderVideos
        );
        
        await SaveHtmlFileToDirectory(item.Name, htmlTemplate);
        _logger.LogSuccess($"Template file for {item.Name} generated successfully.");
    }
}