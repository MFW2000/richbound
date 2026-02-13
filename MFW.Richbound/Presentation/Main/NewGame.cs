using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Main;

public class NewGame : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("New game selected.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.MainMenu;
    }
}
