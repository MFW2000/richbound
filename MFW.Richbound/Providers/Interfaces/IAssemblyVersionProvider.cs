namespace MFW.Richbound.Providers.Interfaces;

/// <summary>
/// Defines a contract for retrieving the application's assembly version.
/// </summary>
public interface IAssemblyVersionProvider
{
    /// <summary>
    /// Retrieves the version of the application's assembly.
    /// </summary>
    /// <returns>The application version or null if the version could not be retrieved.</returns>
    Version? GetVersion();

    /// <summary>
    /// Retrieves the version of the application's assembly as a string in the format of "major.minor.build".
    /// </summary>
    /// <returns>The formatted application version or an empty string if the version could not be retrieved.</returns>
    string GetFormattedVersion();
}
