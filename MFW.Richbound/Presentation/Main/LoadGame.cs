using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Main;

/// <summary>
/// Responsible for assisting the user in loading a saved game.
/// </summary>
public class LoadGame(ISaveFileManager saveFileManager, IGameState gameState) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== Load Game ===");

        if (!saveFileManager.HasSaveFile())
        {
            Console.WriteLine("No save game found. Returning to main menu.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.MainMenu;
        }

        Console.WriteLine("Loading save game...");

        var saveGame = saveFileManager.LoadGame();

        if (saveGame == null)
        {
            Console.WriteLine("Something went wrong while loading the save game. Returning to main menu.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.MainMenu;
        }

        gameState.Initialize(saveGame);

        Console.WriteLine("Save game loaded successfully.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.GameMenu;
    }
}
