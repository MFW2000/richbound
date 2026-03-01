using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Game;

public class GameMenu(IGameState gameState, ISaveFileManager saveFileManager, IConsoleLogger logger) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== Placeholder Menu ===");
        Console.WriteLine("What do you want to do?");
        Console.WriteLine();
        Console.WriteLine("--- Statistics ---");
        Console.WriteLine("Nothing here yet...");
        Console.WriteLine();
        Console.WriteLine("--- Options ---");
        Console.WriteLine("1.  Add $100 to your wallet.");
        Console.WriteLine("2.  Remove $100 from your wallet.");
        Console.WriteLine("3.  Deposit $100 into your bank account.");
        Console.WriteLine("4.  Withdraw $100 from your bank account.");
        Console.WriteLine("5.  Eat a meal");
        Console.WriteLine("6.  Drink a beverage");
        Console.WriteLine("7.  Skip a day");
        Console.WriteLine("8.  Save game");
        Console.WriteLine("9.  Save game and exit");
        Console.WriteLine("10. Exit without saving");
        Console.WriteLine("11. Return to main menu");
        Console.WriteLine("Select an option [1-11]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 11);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                continue;
            }

            bool confirm;

            switch (input)
            {
                case 10:
                    confirm = PromptYesNo("Are you sure you want to exit without saving [y/N]?", false);

                    return confirm ? null : PromptType.GameMenu;
                case 11:
                    confirm = PromptYesNo("Are you sure you want to return to the main menu without saving [y/N]?", false);

                    return confirm ? PromptType.MainMenu : PromptType.GameMenu;
                default:
                    Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                    logger.LogWarning($"Invalid menu option selected with input '{input}' that should never be reached.");

                    break;
            }
        }
    }
}
