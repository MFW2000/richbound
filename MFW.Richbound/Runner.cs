using MFW.Richbound.Enumerations;
using MFW.Richbound.Factories.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation;

namespace MFW.Richbound;

/// <summary>
/// Provides the main execution loop for the Password Generator application.
/// </summary>
public class Runner(IPromptFactory promptFactory, IConsoleWrapper consoleWrapper)
{
    /// <summary>
    /// Executes the main loop of the application.
    /// </summary>
    public void Run()
    {
        Prompt? currentPrompt = promptFactory.CreatePrompt<MainMenu>();

        while (currentPrompt is not null)
        {
            consoleWrapper.Clear();

            var promptResult = currentPrompt.DisplayMainPrompt();

            currentPrompt = GetNextPrompt(promptResult);
        }
    }

    /// <summary>
    /// Retrieves the next <see cref="Prompt"/> instance based on the specified prompt type.
    /// </summary>
    /// <param name="promptType">
    /// The <see cref="PromptType"/> value that determines which <see cref="Prompt"/> to create, or null if no specific
    /// prompt is requested.
    /// </param>
    /// <returns>
    /// A new <see cref="Prompt"/> instance corresponding to the specified <see cref="PromptType"/>, or null if the
    /// prompt type is unrecognized or null.
    /// </returns>
    private Prompt? GetNextPrompt(PromptType? promptType)
    {
        return promptType switch
        {
            PromptType.MainMenu => promptFactory.CreatePrompt<MainMenu>(),
            _ => null
        };
    }
}
