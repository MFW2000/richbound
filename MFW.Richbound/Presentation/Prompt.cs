using MFW.Richbound.Enumerations;

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
}
