using HtmlTemplateGenerator.Builder;
using HtmlTemplateGenerator.Models;

namespace HtmlTemplateGenerator.Services;

public class TemplateService
{
    private readonly FileManagerService _fileManager = new();
    private readonly LoggerService _logger = new();
    private readonly TemplateBuilder _templateBuilder = new();
    
    private ProductListing? GetProductListing(string data)
    {
        var productListing = _fileManager.DeserializeYamlFile<ProductListing>(data);

        if (productListing?.Specification?.Text != null)
        {
            productListing.Specification.Items =
                productListing.Specification.GenerateSpecificationItems(productListing.Specification.Text);
        }

        return productListing;
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
        
        var productListing = GetProductListing(data);
        
        if (productListing is null)
        {
            _logger.LogError("Failed to deserialize Yaml data.");
            return;
        }
        
        var shouldRenderSpecification = productListing.Specification is not null && productListing.Specification.Items.Any();
        var shouldRenderVideos = productListing.Videos.Any();
        var shouldRenderArrangementPhoto = productListing.ArrangementPhoto is not null;
        
        _logger.LogInformation("Generating template file...");
        
        if (!shouldRenderSpecification)
        {
            _logger.LogInformation("No specification items found.");
        }
        
        if (!shouldRenderVideos)
        {
            _logger.LogInformation("No videos found.");
        }
        
        if (!shouldRenderArrangementPhoto)
        {
            _logger.LogInformation("No arrangement photo found.");
        }
        
        var htmlTemplate = _templateBuilder.GenerateHtmlTemplate(
            productListing, 
            shouldRenderSpecification, 
            shouldRenderVideos, 
            shouldRenderArrangementPhoto
        );
        
        await SaveHtmlFileToDirectory(productListing.Name, htmlTemplate);
        _logger.LogSuccess($"Template file for {productListing.Name} generated successfully.");
    }
}