using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Providers.Interfaces;

namespace MFW.Richbound.Presentation;

/// <summary>
/// Responsible for assisting the user in navigating the main menu.
/// </summary>
public class MainMenu(IAssemblyVersionProvider assemblyVersionProvider, IConsoleLogger logger) : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine(GetFormattedVersion());

        return null;
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

        return $"v{version.ToString(3)}";
    }
}
