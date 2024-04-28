using eMagTemplateGenerator.Html;
using eMagTemplateGenerator.Models;
using eMagTemplateGenerator.Static;
using Newtonsoft.Json;

namespace eMagTemplateGenerator.Services;

public class TemplateService
{
    private string _jsonData = "";
    public ProductListing? ProductListing { get; private set; }
    private readonly FileManagerService _fileManagerService = new();
    private readonly LoggerService _logger = new();
    
    public async Task ReadJsonData(string fileName)
    {
        _jsonData = await File.ReadAllTextAsync(fileName);
    }
    
    public ProductListing? DeserializeJsonData(string jsonData)
    {
        ProductListing = JsonConvert.DeserializeObject<ProductListing>(jsonData);
        if (ProductListing is not null)
        {
            ProductListing.Specification.Items =
                ProductListing.Specification.GenerateSpecificationItems(ProductListing.Specification.Text);
        }

        return ProductListing;
    }
    
    public string GenerateHtml(ProductListing productListing)
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

        if (productListing.Specification.Items.Any())
        {
            html = html
                .H2(productListing.Specification.Title)
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

    public async Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Szablony")
    {
        await _fileManagerService.WriteAndSaveHtmlFileToDirectory(fileName, htmlPage, directoryName);
    }
    
    public async Task GenerateHtmlTemplateFile(string jsonFileName)
    {
        await ReadJsonData(jsonFileName);
        
        var productListing = DeserializeJsonData(_jsonData);
        
        if (productListing is null)
        {
            throw new Exception("Failed to deserialize JSON data.");
        }
        
        _logger.LogInformation("Generating template file...");
        var htmlPage = GenerateHtml(productListing);
        
        await SaveHtmlFileToDirectory(productListing.Name, htmlPage);
        _logger.LogInformation($"Template file for {productListing.Name} generated successfully.");
    }
}