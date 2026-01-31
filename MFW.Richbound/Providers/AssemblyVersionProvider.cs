using System.Reflection;
using MFW.Richbound.Providers.Interfaces;

namespace MFW.Richbound.Providers;

/// <summary>
/// Implements <see cref="IAssemblyVersionProvider"/> for retrieving the application's assembly version.
/// </summary>
/// <param name="assembly">The assembly whose version is retrieved.</param>
public class AssemblyVersionProvider(Assembly assembly) : IAssemblyVersionProvider
{
    /// <inheritdoc/>
    public Version? GetVersion()
    {
        return assembly.GetName().Version;
    }

    /// <inheritdoc/>
    public string GetFormattedVersion()
    {
        var version = GetVersion();

        if (version is null)
        {
            return string.Empty;
        }

        return version.ToString(3);
    }
}
