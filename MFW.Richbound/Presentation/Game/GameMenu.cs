using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Game;

public class GameMenu : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== Menu ===");
        Console.WriteLine("What do you want to do?");
        Console.WriteLine();
        Console.WriteLine("Nothing here yet... Returning to the main menu.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.MainMenu;
    }
}
