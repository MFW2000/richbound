using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Presentation.Game.Main;

public class CharacterMenu(ISaveFileManager saveFileManager, IGameState gameState, IConsoleLogger logger) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== Character ===");
        Console.WriteLine(gameState.FullName);
        Console.WriteLine();
        Console.WriteLine("--- Status ---");
        Console.WriteLine("Nothing here yet...");
        Console.WriteLine();
        Console.WriteLine("--- Options ---");
        Console.WriteLine("1. Close");
        Console.WriteLine("2. Inventory");
        Console.WriteLine("3. Save Game");
        Console.WriteLine("4. Exit Game");
        Console.WriteLine("5. Main Menu");
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
                case 2:
                    return PromptType.CharacterMenu;
                case 3:
                    return PromptSaveGame();
                case 4:
                    return PromptLeaveGame(null);
                case 5:
                    return PromptLeaveGame(PromptType.MainMenu);
            }
        }
    }

    private PromptType PromptSaveGame()
    {
        Console.WriteLine();

        TrySaveGameAndNotify();

        Console.WriteLine();

        ContinuePrompt();

        return PromptType.CharacterMenu;
    }

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

    private bool TrySaveGameAndNotify()
    {
        var state = gameState.MapToGameStateDto();
        var success = saveFileManager.SaveGame(state);

        if (!success)
        {
            logger.LogError($"Failed to save game from {nameof(CharacterMenu)}.");
        }

        var message = success ? "Game saved successfully." : "Something went wrong while saving your game, aborting.";

        Console.WriteLine(message);

        return success;
    }
}
