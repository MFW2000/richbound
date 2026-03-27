namespace MFW.Richbound.Services.Interfaces;

/// <summary>
/// Defines a contract for handling time-related operations.
/// </summary>
public interface ITimeService
{
    /// <summary>
    /// Updates the game state based on the number of hours passed. Once the time reaches 24 hours, it updates the day
    /// and handles any daily updates.
    /// </summary>
    /// <param name="hoursPassed">The number of hours that have passed since the last update.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the hours passed are less than or equal to 0 or greater than 24.
    /// </exception>
    void PassTime(int hoursPassed);
}
