using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Game;

/// <summary>
/// Responsible for introducing the player to the game.
/// </summary>
public class NewGameIntro : Prompt
{
    /// <inheritdoc/>
    public override Task<PromptType?> DisplayMainPromptAsync()
    {
        Console.WriteLine($"=== Welcome to {DisplayText.CityName}! ===");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine("You’ve walked away from everything you knew, chasing the chance to start over.");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine(
            $"You arrive by plane at {DisplayText.CityName} International Airport with nothing but the clothes on your back.");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine(
            "Begin with odd jobs or petty crime to get by. If things get really tough, you can always start begging.");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine("Once you’ve saved enough money, you can begin building a business empire—legal or illegal.");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine("The choice is yours!");

        Thread.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();

        ContinuePrompt();

        return Task.FromResult<PromptType?>(PromptType.GameMenu);
    }
}
