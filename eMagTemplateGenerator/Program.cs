using eMagTemplateGenerator.Services;

var service = new TemplateService();
var logger = new LoggerService();

try
{
    await service.GenerateHtmlTemplateFile("templateData.json");
    logger.LogInformation("Press any key to exit.", true);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
