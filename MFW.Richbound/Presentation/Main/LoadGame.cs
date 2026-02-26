using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Main;

public class LoadGame(ISaveFileManager saveFileManager) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("Load game selected.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.MainMenu;
    }
}
