using System.Diagnostics.CodeAnalysis;
using MFW.Richbound.Infrastructure.Interfaces;

namespace MFW.Richbound.Infrastructure;

/// <summary>
/// Implements <see cref="IThreadWrapper"/> for wrapping thread functionality that can be mocked for testing.
/// </summary>
[ExcludeFromCodeCoverage]
public class ThreadWrapper : IThreadWrapper
{
    /// <inheritdoc/>
    public void Sleep(int millisecondsTimeout)
    {
        Thread.Sleep(millisecondsTimeout);
    }
}
