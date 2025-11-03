using System.Diagnostics.CodeAnalysis;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Infrastructure;

/// <summary>
/// Implements <see cref="IConsoleWrapper"/> for wrapping console functionality that can be mocked for testing.
/// </summary>
[ExcludeFromCodeCoverage]
public class ConsoleWrapper : IConsoleWrapper
{
    /// <inheritdoc/>
    public void Clear()
    {
        Console.Clear();
    }
}
