using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation.Game;

public class GameMenu : Prompt
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
        Console.WriteLine("5.  Buy a burger");
        Console.WriteLine("6.  Buy a drink");
        Console.WriteLine("7.  Skip a day");
        Console.WriteLine("8.  Save Game");
        Console.WriteLine("9.  Exit and save game");
        Console.WriteLine("10. Exit without saving");
        Console.WriteLine("Select an option [1-10]:");

        ContinuePrompt();

        return PromptType.MainMenu;
    }
}
