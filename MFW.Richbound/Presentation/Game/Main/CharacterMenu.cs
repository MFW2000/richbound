using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Game.Main;

/// <summary>
/// Responsible for showing player character information and providing options for interacting with the character.
/// This also includes the ability to save and exit the game.
/// </summary>
public class CharacterMenu(
    ISaveFileManager saveFileManager,
    IGameState gameState,
    IGameStateMapper mapper,
    IConsoleLogger logger)
    : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== Character ===");
        Console.WriteLine(gameState.FullName);
        Console.WriteLine($"Day {gameState.Day}, {gameState.TimeText}");
        Console.WriteLine();

        DisplayStatus(gameState);

        Console.WriteLine();
        Console.WriteLine("--- Options ---");
        Console.WriteLine("1. Close");
        Console.WriteLine("2. Inventory");
        Console.WriteLine("3. Save Game");
        Console.WriteLine("4. Main Menu");
        Console.WriteLine("5. Exit Game");
        Console.WriteLine("Select an option [1-5]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 5);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                continue;
            }

            switch (input)
            {
                case 1:
                    return gameState.LastLocation;
                case 2:
                    return PromptType.CharacterMenu;
                case 3:
                    return PromptSaveGame();
                case 4:
                    return PromptLeaveGame(PromptType.MainMenu);
                case 5:
                    return PromptLeaveGame(null);
            }
        }
    }

    /// <summary>
    /// Prompt the user to save their game.
    /// </summary>
    /// <returns>Back to the <see cref="CharacterMenu"/> prompt.</returns>
    private PromptType PromptSaveGame()
    {
        Console.WriteLine();

        TrySaveGameAndNotify();

        Console.WriteLine();

        ContinuePrompt();

        return PromptType.CharacterMenu;
    }

    /// <summary>
    /// Prompt the user to confirm if they want to save their game before exiting.
    /// </summary>
    /// <param name="destination">The target destination.</param>
    /// <returns>The destination prompt to return to after saving or exiting.</returns>
    private PromptType? PromptLeaveGame(PromptType? destination)
    {
        var confirm = PromptYesNo("Do you want to save your game before exiting? [Y/n]:", true);

        if (!confirm)
        {
            return destination;
        }

        Console.WriteLine();

        var success = TrySaveGameAndNotify();

        Console.WriteLine();

        ContinuePrompt();

        return success ? destination : PromptType.CharacterMenu;
    }

    /// <summary>
    /// Attempt to save the game and notify the user of the result.
    /// </summary>
    /// <returns>True if the game was saved successfully, otherwise false.</returns>
    private bool TrySaveGameAndNotify()
    {
        var success = saveFileManager.SaveGame(mapper.MapToDto(gameState));

        if (!success)
        {
            logger.LogError($"Failed to save game from {nameof(CharacterMenu)}.");
        }

        var message = success ? "Game saved successfully." : "Something went wrong while saving your game, aborting.";

        Console.WriteLine(message);

        return success;
    }
}
