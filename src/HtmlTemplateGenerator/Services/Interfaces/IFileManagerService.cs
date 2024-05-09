namespace HtmlTemplateGenerator.Services.Interfaces;

public interface IFileManagerService
{
    bool CheckIfFileExists(string fileName);
    Task WriteAndSaveHtmlFileToDirectory(string fileName, string htmlPage, string directoryName);
    string CreateDirectory(string directoryName);
    T? DeserializeYamlFile<T>(string data);
    Task<string?> ReadFile(string fileName);
}