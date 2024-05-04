using Spectre.Console;

namespace HtmlTemplateGenerator.Services;

public class LoggerService
{
    public void LogInformation(string message, bool addReadLine = false)
    {
        AnsiConsole.MarkupLine($"[lightgoldenrod2_1 italic]Info:[/] {message}");
        
        if (addReadLine)
        {
            Console.ReadLine();
        }
    }
    
    public void LogError(string message, bool addReadLine = false)
    {
        AnsiConsole.MarkupLine($"[indianred1 italic]Error:[/] {message}");
        
        if (addReadLine)
        {
            Console.ReadLine();
        }
    }
    
    public void LogSuccess(string message, bool addReadLine = false)
    {
        AnsiConsole.MarkupLine($"[darkolivegreen3_1 italic]Success:[/] {message}");
        
        if (addReadLine)
        {
            Console.ReadLine();
        }
    }

    public async Task LogStatus(string message, Func<StatusContext, Task> status)
    {
        await AnsiConsole.Status()
            .StartAsync(message, async ctx =>
            {
                await status(ctx);
            });
    }
}