using HtmlTemplateGenerator.Builder;
using HtmlTemplateGenerator.Exceptions;
using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Services.Interfaces;
using HtmlTemplateGenerator.Static;
using HtmlTemplateGenerator.Validation;

namespace HtmlTemplateGenerator.Services;

public class TemplateService : ITemplateService
{
    private readonly IFileManagerService _fileManager = new FileManagerService();
    private readonly ILoggerService _logger = new LoggerService();
    private readonly TemplateBuilder _templateBuilder = new();
    private readonly ItemValidator _itemValidator = new();

    public Item? GetItem(string data)
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
        catch (WrongSpecificationItemFormatException e)
        {
            _logger.LogError($"Failed to generate specification items: {e.Message}");
            return null;
        }

        return item;
    }

    public async Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Templates")
    {
        await _fileManager.WriteAndSaveHtmlFileToDirectory(fileName, htmlPage, directoryName);
    }
    
    public RenderFlags GetRenderFlags(Item item)
        => new()
        {
            ShouldRenderBannerImage = _itemValidator.ShouldRenderBannerImage(item?.BannerImageSrc),
            ShouldRenderHeader = _itemValidator.ShouldRenderHeader(item?.Header),
            ShouldRenderDescriptions = _itemValidator.ShouldRenderDescriptions(item?.Descriptions),
            ShouldRenderSpecification = _itemValidator.ShouldRenderSpecification(item?.Specification),
            ShouldRenderVideos = _itemValidator.ShouldRenderVideos(item?.Videos)
        };
    
    public async Task GenerateHtmlTemplateFile(string fileName)
    {
        if (!_fileManager.CheckIfFileExists(fileName))
        {
            return;
        }

        var fileContent = await _fileManager.ReadFile(fileName);
        if (fileContent is null)
        {
            return;
        }

        var item = GetItem(fileContent);
        if (!_itemValidator.ValidateItem(item) || !_itemValidator.ValidateItemName(item?.Name))
        {
            return;
        }

        var renderFlags = GetRenderFlags(item);
        var htmlTemplate = _templateBuilder.GenerateHtmlTemplate(item, renderFlags);

        await _logger.LogStatus("Generating template file...", async _ =>
        {
            await SaveHtmlFileToDirectory(item.Name, htmlTemplate);
        });
        _logger.LogSuccess($"Template file for {item.Name} generated successfully.");
    }
}
