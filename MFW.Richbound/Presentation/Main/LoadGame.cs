using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Main;

public class LoadGame(ISaveFileManager saveFileManager) : Prompt
{
    /// <inheritdoc/>
    public override Task<PromptType?> DisplayMainPromptAsync()
    {
        Console.WriteLine("Load game selected.");
        Console.WriteLine();

        ContinuePrompt();

        return Task.FromResult<PromptType?>(PromptType.MainMenu);
    }
}
