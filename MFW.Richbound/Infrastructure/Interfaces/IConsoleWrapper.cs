namespace MFW.Richbound.Infrastructure.Interfaces;

/// <summary>
/// Defines a contract for wrapping console functionality that can be mocked for testing.
/// </summary>
public interface IConsoleWrapper
{
    /// <summary>
    /// Wrapper for the <see cref="Console.Clear"/> method.
    /// </summary>
    void Clear();
}
