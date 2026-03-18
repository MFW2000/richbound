using System.Text.RegularExpressions;
using MFW.Richbound.Exceptions.Prompt;

namespace MFW.Richbound.Helpers;

/// <summary>
/// Provides helper methods for handling console input and other prompt-related functionalities.
/// </summary>
public static class PromptHelper
{
    /// <summary>
    /// Reads a string input from the console with optional trimming, length validation, and empty value handling.
    /// </summary>
    /// <param name="allowEmpty">Specifies whether the input string is allowed to be empty.</param>
    /// <param name="trim">Indicates whether the input string should be trimmed.</param>
    /// <param name="maxLength">The maximum allowable length for the input string.</param>
    /// <param name="matchRegex">A regular expression pattern to match against the input string.</param>
    /// <returns>The processed string input from the console.</returns>
    /// <exception cref="InputEmptyException">
    /// Thrown if the input is empty and empty input is not allowed.
    /// </exception>
    /// <exception cref="InputOutOfRangeException">
    /// Thrown if the input exceeds the maximum allowable length.
    /// </exception>
    /// <exception cref="InputRegexMismatchException">
    /// Thrown if the input does not match the specified regular expression pattern.
    /// </exception>
    public static string ReadString(bool allowEmpty = false, bool trim = true, int? maxLength = null, Regex? matchRegex = null)
    {
        var input = Console.ReadLine() ?? string.Empty;

        if (!allowEmpty && string.IsNullOrWhiteSpace(input))
        {
            throw new InputEmptyException();
        }

        if (trim)
        {
            input = input.Trim();
        }

        if (input.Length > maxLength)
        {
            throw new InputOutOfRangeException();
        }

        if (matchRegex != null && !matchRegex.IsMatch(input))
        {
            throw new InputRegexMismatchException();
        }

        return input;
    }

    /// <summary>
    /// Reads an integer input from the console with optional range validation.
    /// </summary>
    /// <param name="allowEmpty">Specifies whether the input string is allowed to be empty.</param>
    /// <param name="minRange">The minimum allowable value for the input integer.</param>
    /// <param name="maxRange">The maximum allowable value for the input integer.</param>
    /// <returns>The processed integer input from the console.</returns>
    /// <exception cref="FormatException">Thrown if the input is not a valid integer.</exception>
    /// <exception cref="InputEmptyException">
    /// Thrown if the input is empty and empty input is not allowed.
    /// </exception>
    /// <exception cref="InputOutOfRangeException">
    /// Thrown if the input is outside the allowable range.
    /// </exception>
    public static int? ReadInt(bool allowEmpty = false, int? minRange = null, int? maxRange = null)
    {
        var input = ReadString(allowEmpty: allowEmpty);

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (!int.TryParse(input, out var result))
        {
            throw new FormatException("Input is not a valid integer.");
        }

        if (result < minRange || result > maxRange)
        {
            throw new InputOutOfRangeException();
        }

        return result;
    }
}
