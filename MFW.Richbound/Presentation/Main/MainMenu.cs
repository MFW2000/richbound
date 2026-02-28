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
    public override Task<PromptType?> DisplayMainPromptAsync()
    {
        Console.WriteLine($"=== {DisplayText.AppTitle}{GetFormattedVersion()} ===");
        Console.WriteLine(DisplayText.AppSubtitle);
        Console.WriteLine();
        Console.WriteLine("--- Main Menu ---");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Load Game");
        Console.WriteLine("3. Exit");
        Console.WriteLine("Select an option [1-3]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 3);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                continue;
            }

            switch (input)
            {
                case 1:
                    return Task.FromResult<PromptType?>(PromptType.NewGame);
                case 2:
                    return Task.FromResult<PromptType?>(PromptType.LoadGame);
                case 3:
                    return Task.FromResult<PromptType?>(null);
                default:
                    Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                    logger.LogWarning(
                        $"Invalid menu option selected with input '{input}' that should never be reached.");

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
