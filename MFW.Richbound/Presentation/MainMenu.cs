using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Presentation;

/// <summary>
/// Responsible for assisting the user in navigating the main menu.
/// </summary>
public class MainMenu : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine("Hello, World!");

        return null;
    }
}
