using Spectre.Console;

namespace HtmlTemplateGenerator.Services.Interfaces;

public interface ILoggerService
{
    void LogInformation(string message, bool addReadLine = false);
    void LogError(string message, bool addReadLine = false);
    void LogSuccess(string message, bool addReadLine = false);
    Task LogStatus(string message, Func<StatusContext, Task> status);
}