using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Game;

/// <summary>
/// Responsible for introducing the player to the game.
/// </summary>
public class GameIntro(IThreadWrapper threadWrapper) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== Welcome to {DisplayText.CityName}! ===");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine("You’ve walked away from everything you knew, chasing the chance to start over.");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine(
            $"You arrive by plane at {DisplayText.CityName} International Airport with nothing but the clothes on your back.");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine(
            "Begin with odd jobs or petty crime to get by. If things get really tough, you can always start begging.");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine("Once you’ve saved enough money, you can begin building a business empire—legal or illegal.");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();
        Console.WriteLine("The choice is yours!");

        threadWrapper.Sleep(Constants.DisplayDelayTimeMilliseconds);

        Console.WriteLine();

        ContinuePrompt();

        return PromptType.CharacterMenu;
    }
}
