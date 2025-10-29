using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;

namespace MFW.Richbound.Presentation;

/// <summary>
/// Responsible for assisting the user in navigating the game's main menu.
/// </summary>
public class MainMenu : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== {CommonText.AppTitle} ===");
        Console.WriteLine(CommonText.AppSubTitle);
        Console.WriteLine();
        Console.WriteLine("--- Main Menu ---");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Load Game");
        Console.WriteLine("3. Exit");
        Console.WriteLine(CommonText.TooltipOption);

        while (true)
        {
            Console.Write(CommonText.InputPrompt);

            var input = PromptHelper.ReadString(true);

            switch (input.ToLower())
            {
                case "1":
                case "2":
                    Console.WriteLine("Coming soon!");
                    break;
                case "3":
                    return null;
                default:
                    Console.WriteLine("Please select a valid menu option.");
                    break;
            }
        }
    }
}
