using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Exceptions.Prompt;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Infrastructure.Utility;
using MFW.Richbound.Models;

namespace MFW.Richbound.Presentation.Main;

public class NewGame(ISaveFileManager saveFileManager, IGameState gameState) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== New Game ===");
        Console.WriteLine("Start a new game.");
        Console.WriteLine();

        if (saveFileManager.HasSaveFile())
        {
            Console.WriteLine("A save file already exists.");

            var overwriteSave = PromptYesNo("Do you wish to overwrite it [y/N]:", false);

            if (!overwriteSave)
            {
                return PromptType.MainMenu;
            }
        }

        var firstName = PromptFirstName();













        // TODO:
        //  1. Check if a save already exists and ask if to overwrite
        //  2. Guide through the new character creation process
        //  3. End with flavour text and move to the in game menu

        var newGameState = new GameStateDto("John", "Doe", 100, 100, 100, 0, 0);

        gameState.Initialize(newGameState);

        Console.WriteLine($"Name: {gameState.FirstName} {gameState.LastName}");
        Console.WriteLine($"Health: %{gameState.Health}");
        Console.WriteLine($"Hunger: %{gameState.Hunger}");
        Console.WriteLine($"Thirst: %{gameState.Thirst}");
        Console.WriteLine($"Money in pocket: ${gameState.PocketMoney}");
        Console.WriteLine($"Bank Balance: ${gameState.BankBalance}");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.MainMenu;
    }

    private static string PromptFirstName()
    {
        Console.WriteLine("First name: ");

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            try
            {
                return PromptHelper.ReadString(false, true, 20, RegexUtility.UnicodeLetterRegex());
            }
            catch (InputEmptyException)
            {
                Console.WriteLine("Your character must have a first name.");
            }
            catch (InputOutOfRangeException)
            {
                Console.WriteLine("Your character's first name cannot be longer than 20 characters.");
            }
            catch (InputRegexMismatchException)
            {
                Console.WriteLine("Your character's first must contain a letter.");
            }
        }
    }
}
