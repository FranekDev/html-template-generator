using HtmlTemplateGenerator.Services;

var service = new TemplateService();
var logger = new LoggerService();

await service.GenerateHtmlTemplateFile("templateData.yaml");
logger.LogInformation("Press any key to exit.", true);
