using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Providers.Interfaces;

namespace MFW.Richbound.Presentation;

/// <summary>
/// Responsible for assisting the user in navigating the game's main menu.
/// </summary>
public class MainMenu(IAssemblyVersionProvider assemblyVersionProvider, IConsoleLogger logger) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== {CommonText.AppTitle}{GetAssemblyVersion()} ===");
        Console.WriteLine(CommonText.AppSubtitle);
        Console.WriteLine();
        Console.WriteLine("--- Main Menu ---");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Load Game");
        Console.WriteLine("3. Exit");
        Console.WriteLine(CommonText.TooltipOption);

        while (true)
        {
            Console.Write(CommonText.InputPrompt);

            var input = PromptHelper.ReadString(true);

            switch (input.ToLower())
            {
                case "1":
                case "2":
                    Console.WriteLine("Coming soon!");
                    break;
                case "3":
                    return null;
                default:
                    Console.WriteLine("Please select a valid menu option.");
                    break;
            }
        }
    }

    /// <summary>
    /// Retrieves the application's assembly version formatted to be shown to the user. The version itself will be
    /// shown in the format of "major.minor.build".
    /// </summary>
    /// <returns>The formatted application version or an empty string if the version could not be retrieved.</returns>
    private string GetAssemblyVersion()
    {
        var version = assemblyVersionProvider.GetVersion();

        if (version is not null)
        {
            return $" v{version.ToString(3)}";
        }

        logger.LogError("Unable to retrieve the application version.");

        return string.Empty;
    }
}
