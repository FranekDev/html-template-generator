namespace eMagTemplateGenerator.Services;

public class LoggerService
{
    public void LogInformation(string message, bool addReadLine = false)
    {
        Console.WriteLine(message);
        
        if (addReadLine)
        {
            Console.ReadLine();
        }
    }
}