using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Game;

/// <summary>
/// Responsible for introducing the player to the game.
/// </summary>
public class NewGameIntro : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== Welcome to {DisplayText.CityName}! ===");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine("You’ve walked away from everything you knew, chasing the chance to start over.");
        Console.WriteLine();

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine(
            $"You arrive by plane at {DisplayText.CityName} International Airport with nothing but the clothes on your back.");
        Console.WriteLine();

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine(
            "Begin with odd jobs or petty crime to get by. If things get really tough, you can always start begging.");
        Console.WriteLine();

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine("Once you’ve saved enough money, you can begin building a business empire—legal or illegal.");
        Console.WriteLine();

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine("The choice is yours!");
        Console.WriteLine();

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        ContinuePrompt();

        return PromptType.GameMenu;
    }
}
