using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Game;

public class NewGameIntro : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== Welcome to {DisplayText.AppTitle}! ===");
        Console.WriteLine("Nothing here yet...");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.GameMenu;
    }
}
