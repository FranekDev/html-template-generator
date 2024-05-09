using HtmlTemplateGenerator.Models;

namespace HtmlTemplateGenerator.Services.Interfaces;

public interface ITemplateService
{
    Item? GetItem(string data);
    Task SaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName = "Templates");
    RenderFlags GetRenderFlags(Item item);
    Task GenerateHtmlTemplateFile(string fileName);
}