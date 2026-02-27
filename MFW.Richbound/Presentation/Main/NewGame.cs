using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Exceptions.Prompt;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Infrastructure.Utility;
using MFW.Richbound.Models;

namespace MFW.Richbound.Presentation.Main;

/// <summary>
/// Responsible for assisting the user in creating a new character and starting a new game.
/// </summary>
public class NewGame(ISaveFileManager saveFileManager, IGameState gameState) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("=== New Game ===");

        // TODO: Add a new line after choosing to overwrite the save file.
        var promptOverwriteSave = PromptOverwriteSave();
        if (!promptOverwriteSave)
        {
            return PromptType.MainMenu;
        }

        Console.WriteLine("Start a new game by creating a new character.");
        Console.WriteLine();

        var gender = PromptGender();
        var lastName = PromptLastName();
        var firstName = PromptFirstName();

        var promptCharacterConfirmation = PromptCharacterConfirmation(firstName, lastName, gender);
        if (!promptCharacterConfirmation)
        {
            return PromptType.NewGame;
        }

        var newGameState = new GameStateDto(gender, firstName, lastName, 100, 100, 100, 0, 0);

        gameState.Initialize(newGameState);

        Console.WriteLine();
        Console.WriteLine("Your character has been created.");
        Console.WriteLine();

        ContinuePrompt();

        return PromptType.NewGameIntro;
    }

    /// <summary>
    /// Check if the user wants to overwrite the save file if it exists.
    /// </summary>
    /// <returns>True if the user wants to overwrite the save file, otherwise false.</returns>
    private bool PromptOverwriteSave()
    {
        if (!saveFileManager.HasSaveFile())
        {
            return true;
        }

        Console.WriteLine("A save file already exists.");

        var overwriteSave = PromptYesNo("Do you wish to overwrite it [y/N]:", false);

        return overwriteSave;
    }

    /// <summary>
    /// Prompt the user if they want to create a male or female character.
    /// </summary>
    /// <returns>A <see cref="Gender"/> value representing the user's gender.</returns>
    private static Gender PromptGender()
    {
        Console.WriteLine("Choose your gender (male/female) [m/f]:");

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            try
            {
                var input = PromptHelper.ReadString();

                if (input.Equals("m", StringComparison.OrdinalIgnoreCase)
                    || input.Equals("male", StringComparison.OrdinalIgnoreCase))
                {
                    return Gender.Male;
                }

                if (input.Equals("f", StringComparison.OrdinalIgnoreCase)
                    || input.Equals("female", StringComparison.OrdinalIgnoreCase))
                {
                    return Gender.Female;
                }

                Console.WriteLine("Please enter 'male' (m) or 'female' (f).");
            }
            catch (InputEmptyException)
            {
                Console.WriteLine("You must enter a gender.");
            }
        }
    }

    /// <summary>
    /// Prompt the user for their character's last name.
    /// </summary>
    /// <returns>The user's character's last name.</returns>
    private static string PromptLastName()
    {
        Console.WriteLine($"What is your last name (max {Constants.MaxNameLength} characters):");

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            try
            {
                return PromptHelper.ReadString(
                    maxLength: Constants.MaxNameLength,
                    matchRegex: RegexUtility.UnicodeLetterRegex());
            }
            catch (InputEmptyException)
            {
                Console.WriteLine("Your character must have a last name.");
            }
            catch (InputOutOfRangeException)
            {
                Console.WriteLine(
                    $"Your character's last name cannot be longer than {Constants.MaxNameLength} characters.");
            }
            catch (InputRegexMismatchException)
            {
                Console.WriteLine("Your character's last name must at least contain one letter.");
            }
        }
    }

    /// <summary>
    /// Prompt the user for their character's first name.
    /// </summary>
    /// <returns>The user's character's first name.</returns>
    private static string PromptFirstName()
    {
        Console.WriteLine($"What is your first name (max {Constants.MaxNameLength} characters):");

        while (true)
        {
            Console.Write(DisplayText.InputPrompt);

            try
            {
                return PromptHelper.ReadString(
                    maxLength: Constants.MaxNameLength,
                    matchRegex: RegexUtility.UnicodeLetterRegex());
            }
            catch (InputEmptyException)
            {
                Console.WriteLine("Your character must have a first name.");
            }
            catch (InputOutOfRangeException)
            {
                Console.WriteLine(
                    $"Your character's first name cannot be longer than {Constants.MaxNameLength} characters.");
            }
            catch (InputRegexMismatchException)
            {
                Console.WriteLine("Your character's first name must at least contain one letter.");
            }
        }
    }

    /// <summary>
    /// Prompt the user to confirm their character's details.
    /// </summary>
    /// <param name="firstName">The chosen first name.</param>
    /// <param name="lastName">The chosen last name.</param>
    /// <param name="gender">The chosen gender.</param>
    /// <returns>True if the user confirms their character's details, otherwise false.</returns>
    private static bool PromptCharacterConfirmation(string firstName, string lastName, Gender gender)
    {
        var promptText = gender == Gender.Male
            ? $"You are Mr. {firstName} {lastName}. Is this correct? [y/n]:"
            : $"You are Mrs. {firstName} {lastName}. Is this correct? [y/n]:";

        return PromptYesNo(promptText);
    }
}
