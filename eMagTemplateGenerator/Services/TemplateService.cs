using eMagTemplateGenerator.Html;
using eMagTemplateGenerator.Models;
using eMagTemplateGenerator.Static;

namespace eMagTemplateGenerator.Services;

public class TemplateService
{
    private ProductListing? _productListing;
    private readonly FileManagerService _fileManager = new();
    private readonly LoggerService _logger = new();
    
    private ProductListing? DeserializeYamlData(string data)
    {
        _productListing = _fileManager.DeserializeYamlFile<ProductListing>(data);

        if (_productListing?.Specification?.Text != null)
        {
            _productListing.Specification.Items =
                _productListing.Specification.GenerateSpecificationItems(_productListing.Specification.Text);
        }

        return _productListing;
    }
    

    private string GenerateHtml(ProductListing productListing)
    {
        var html = new HtmlBuilder();

        html = html
            .TableOpen()
            .TableBodyOpen()
            .TableRowOpen()
            .TableCellOpen(2)
            .Img(productListing.CompanyBanner)
            .TableCellClose()
            .TableRowClose()
            .TableRowOpen()
            .TableCellOpen(2)
            .H2("")
            .TableCellClose()
            .TableRowClose()
            .TrItems(productListing.Descriptions)
            .TableBodyClose()
            .TableClose();

        if (productListing.Videos.Any())
        {
            html = productListing.Videos.Aggregate(html, (current, video) => current
                .H2(video.Title)
                .P(video.Description)
                .P(HtmlTag.Video(video.Url))
            );
        }
        else
        {
            _logger.LogInformation("No videos found.");
        }

        if (productListing.Specification != null && productListing.Specification.Items.Any())
        {
            html = html
                .H2($"{productListing.Specification.Title}:")
                .UlWithLi(productListing.Specification.Items.MapToDict());
        }
        else
        {
            _logger.LogInformation("No specification items found.");
        }
        
        if (productListing.ArrangementPhoto != null)
        {
            html = html
                .H2(HtmlTag.Img(productListing.ArrangementPhoto));
        }
        else
        {
            _logger.LogInformation("No arrangement photo found.");
        }

        return html.ResultHtml;
    }

    private async Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Szablony")
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
        
        var productListing = DeserializeYamlData(data);
        
        if (productListing is null)
        {
            _logger.LogError("Failed to deserialize Yaml data.");
            return;
        }
        
        _logger.LogInformation("Generating template file...");
        var htmlPage = GenerateHtml(productListing);
        
        await SaveHtmlFileToDirectory(productListing.Name, htmlPage);
        _logger.LogSuccess($"Template file for {productListing.Name} generated successfully.");
    }
}