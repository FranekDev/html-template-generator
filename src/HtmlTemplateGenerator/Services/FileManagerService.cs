using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HtmlTemplateGenerator.Services;

public class FileManagerService
{
    private readonly LoggerService _logger = new();
    private readonly string _yamlTemplate = """
       name: name
       
       header: header
       
       bannerImageSrc: bannerImageSrc
       
       descriptions:
         - title: title
           text: text
         - title: title
           text: text
       
       specification:
         title: title
         text: |
           Key: Value
           Another key: Value
       
       videos:
         - title: title
           description: description
           url: videoUrl
       """; 

    public bool CheckIfFileExists(string fileName)
    {
        var path = Directory.GetCurrentDirectory();
        var file = Path.Combine(path, fileName);
        return File.Exists(file);
    }

    public async Task WriteAndSaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName)
    {
        var directory = CreateDirectory(directoryName);

        var newFileName = $"{fileName}.html";

        var currentDirectory = Directory.GetCurrentDirectory();
        var newFilePath = Path.Combine(currentDirectory, newFileName);

        await using var sw = File.CreateText(newFilePath);
        await sw.WriteAsync(htmlPage);
        sw.Close();

        if (File.Exists(newFilePath))
        {
            var destination = Path.Combine(directory, newFileName);
            File.Move(newFilePath, destination, true);
        }
    }

    private string CreateDirectory(string directoryName)
    {
        var path = Directory.GetCurrentDirectory();
        directoryName = Path.Combine(path, directoryName);

        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        return directoryName;
    }

    public T? DeserializeYamlFile<T>(string data)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        try
        {
            return deserializer.Deserialize<T>(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            _logger.LogInformation("Available fields: name, header, bannerImageSrc, descriptions, specification, videos");
            _logger.LogInformation($"Example: \n{_yamlTemplate}");
            return default;
        }
    }

    public async Task<string> ReadFile(string fileName)
    {
        return await File.ReadAllTextAsync(fileName);
    }
}