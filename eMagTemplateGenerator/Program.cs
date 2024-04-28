using eMagTemplateGenerator.Services;

var service = new TemplateService();
var logger = new LoggerService();

await service.GenerateHtmlTemplateFile("templateData.json");
logger.LogInformation("Press any key to exit.", true);