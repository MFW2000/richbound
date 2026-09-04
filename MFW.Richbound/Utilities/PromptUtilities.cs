using MFW.Richbound.Domain.Interfaces;

namespace MFW.Richbound.Utilities;

/// <summary>
/// Provides utility methods for displaying prompts and handling user input.
/// </summary>
public static class PromptUtilities
{
    /// <summary>
    /// Displays a prompt to the user, asking whether they want to continue.
    /// </summary>
    public static void ContinuePrompt()
    {
        Console.WriteLine("Press any key to continue.");
        Console.Write(DisplayText.InputPrompt);
        Console.ReadLine();
    }

    /// <summary>
    /// Display the player's status after performing an activity.
    /// </summary>
    /// <param name="gameState">The game state providing the data to display.</param>
    /// <param name="hoursPassed">The amount of time the activity took to complete.</param>
    public static void DisplayPostActivityStatus(IGameState gameState, int hoursPassed)
    {
        Console.WriteLine($"This action took {hoursPassed} hour(s) to complete.");
        Console.WriteLine();

        DisplayStatus(gameState);

        Console.WriteLine();

        ContinuePrompt();
    }

    /// <summary>
    /// Display the player's status after performing an action.
    /// </summary>
    /// <param name="gameState">The game state providing the data to display.</param>
    /// <param name="message">Result message from performing the action.</param>
    public static void DisplayPostActivityStatus(IGameState gameState, string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();

        DisplayStatus(gameState);

        Console.WriteLine();

        ContinuePrompt();
    }

    /// <summary>
    /// Display the player's current status.
    /// </summary>
    /// <param name="gameState">The game state providing the data to display.</param>
    public static void DisplayStatus(IGameState gameState)
    {
        Console.WriteLine("--- Status ---");
        Console.WriteLine($"Health: {gameState.Health}/{FormatUtilities.HitPoints(Constants.MaxCharacterStatValue)}");
        Console.WriteLine($"Hunger: {gameState.Hunger}%");
        Console.WriteLine($"Energy: {gameState.Energy}%");
        Console.WriteLine($"Cash:   {FormatUtilities.Currency(gameState.PocketMoney)}");
    }

    /// <summary>
    /// Prompts the user for a yes/no response with optional prompt text and default value.
    /// </summary>
    /// <param name="promptText">Optional prompt text to be displayed.</param>
    /// <param name="defaultValue">Optional default value to be returned on empty input.</param>
    /// <returns>True for "yes" or "y", false for "no" or "n" (case-insensitive).</returns>
    public static bool PromptYesNo(string? promptText = null, bool? defaultValue = null)
    {
        if (!string.IsNullOrWhiteSpace(promptText))
        {
            Console.WriteLine(promptText);
        }

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            var input = InputUtilities.ReadString(true);

            if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

            switch (input.ToLower())
            {
                case "yes" or "y":
                    return true;
                case "no" or "n":
                    return false;
            }

            Console.WriteLine("Please enter 'yes' (y) or 'no' (n).");
        }
    }
}
