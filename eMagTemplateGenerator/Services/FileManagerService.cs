namespace eMagTemplateGenerator.Services;

public class FileManagerService
{
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
}