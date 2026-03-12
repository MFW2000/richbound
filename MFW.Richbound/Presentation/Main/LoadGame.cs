using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Main;

// TODO: Finished, write tests next.

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
        Console.WriteLine();

        var saveGameLoaded = SaveGameLoaded();
        if (!saveGameLoaded)
        {
            Console.WriteLine("Something went wrong while loading the save game. Returning to main menu.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.MainMenu;
        }

        Console.WriteLine("Save game loaded successfully.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.CharacterMenu;
    }

    /// <summary>
    /// Attempt to load the save game and initialize the game state.
    /// </summary>
    /// <returns>True if the save game was loaded successfully, otherwise false.</returns>
    private bool SaveGameLoaded()
    {
        var saveGame = saveFileManager.LoadGame();
        if (saveGame is null)
        {
            return false;
        }

        gameState.Initialize(saveGame);

        return true;
    }
}
