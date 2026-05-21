using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;

namespace MFW.Richbound.Presentation;

/// <summary>
/// Defines the structure for prompts.
/// </summary>
public abstract class Prompt
{
    /// <summary>
    /// Display the main prompt and handle the user's input.
    /// </summary>
    /// <returns>Next prompt to navigate to or null to exit the application.</returns>
    public abstract PromptType? DisplayMainPrompt();

    /// <summary>
    /// Displays a prompt to the user, asking whether they want to continue.
    /// </summary>
    protected static void ContinuePrompt()
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
    protected static void DisplayPostActivityStatus(IGameState gameState, int hoursPassed)
    {
        Console.WriteLine($"This action took {hoursPassed} hour(s) to complete.");
        Console.WriteLine();

        DisplayStatus(gameState);

        Console.WriteLine();

        ContinuePrompt();
    }

    /// <summary>
    /// Display the player's current status.
    /// </summary>
    /// <param name="gameState">The game state providing the data to display.</param>
    protected static void DisplayStatus(IGameState gameState)
    {
        Console.WriteLine("--- Status ---");
        Console.WriteLine($"Health: {gameState.Health}%");
        Console.WriteLine($"Hunger: {gameState.Hunger}%");
        Console.WriteLine($"Energy: {gameState.Energy}%");
        Console.WriteLine($"Cash:   ${gameState.PocketMoney:N0}");
    }

    /// <summary>
    /// Prompts the user for a yes/no response with optional prompt text and default value.
    /// </summary>
    /// <param name="promptText">Optional prompt text to be displayed.</param>
    /// <param name="defaultValue">Optional default value to be returned on empty input.</param>
    /// <returns>True for "yes" or "y", false for "no" or "n" (case-insensitive).</returns>
    protected static bool PromptYesNo(string? promptText = null, bool? defaultValue = null)
    {
        if (!string.IsNullOrWhiteSpace(promptText))
        {
            Console.WriteLine(promptText);
        }

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            var input = PromptHelper.ReadString(true);

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
