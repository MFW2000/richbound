namespace MFW.Richbound.Infrastructure.Interfaces;

/// <summary>
/// Defines a contract for wrapping thread functionality that can be mocked for testing.
/// </summary>
public interface IThreadWrapper
{
    /// <summary>
    /// Wrapper for the <see cref="Thread.Sleep(int)"/> method.
    /// </summary>
    /// <param name="millisecondsTimeout">The number of milliseconds for which the thread is suspended.</param>
    void Sleep(int millisecondsTimeout);
}
