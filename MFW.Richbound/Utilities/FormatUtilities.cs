using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Utilities;

/// <summary>
/// Provides utility methods for data formatting.
/// </summary>
public static class FormatUtilities
{
    /// <summary>
    /// Display the game time in a 24-hour format.
    /// </summary>
    public static string Time(int time) => $"{time:D2}:00";

    /// <summary>
    /// Returns the appropriate title for the given gender.
    /// </summary>
    /// <param name="gender">The gender of the character.</param>
    /// <returns>The appropriate title.</returns>
    public static string Title(Gender gender) => gender == Gender.Male ? "Mr." : "Ms.";

    /// <summary>
    /// Format the given value as a currency string.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted currency string.</returns>
    public static string Currency(double value) => $"${value:N0}";

    /// <summary>
    /// Format the given value as a percentage string.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted percentage string.</returns>
    public static string Percentage(int value) => $"{value}%";

    /// <summary>
    /// Format the given value as a hit points string.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>The formatted hit points string.</returns>
    public static string HitPoints(double value) => $"{value} HP";
}
