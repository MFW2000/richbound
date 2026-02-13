using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Providers.Interfaces;

namespace MFW.Richbound.Presentation.Main;

/// <summary>
/// Responsible for assisting the user in navigating the main menu.
/// </summary>
public class MainMenu(IAssemblyVersionProvider assemblyVersionProvider, IConsoleLogger logger) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== {DisplayText.AppTitle}{GetFormattedVersion()} ===");
        Console.WriteLine(DisplayText.AppSubtitle);
        Console.WriteLine();
        Console.WriteLine("--- Main Menu ---");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Load Game");
        Console.WriteLine("3. Exit");
        Console.WriteLine(DisplayText.TooltipOption);

        while (true)
        {
            int? input = null;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 3);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);
            }

            switch (input)
            {
                case 1:
                    return PromptType.NewGame;
                case 2:
                    return PromptType.LoadGame;
                case 3:
                    return null;
                default:
                    Console.WriteLine(DisplayText.TooltipInvalidMenuOption);
                    break;
            }
        }
    }

    /// <summary>
    /// Retrieves the version of the application's assembly as a string in the format of "major.minor.build".
    /// </summary>
    /// <returns>The formatted application version or an empty string if the version could not be retrieved.</returns>
    private string GetFormattedVersion()
    {
        var version = assemblyVersionProvider.GetVersion();

        if (version is null)
        {
            logger.LogWarning("Unable to retrieve application version.");

            return string.Empty;
        }

        return $" v{version.ToString(3)}";
    }
}
