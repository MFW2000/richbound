using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Main;

public class LoadGame : Prompt
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
