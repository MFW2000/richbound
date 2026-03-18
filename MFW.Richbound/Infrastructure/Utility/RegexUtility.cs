using System.Text.RegularExpressions;

namespace MFW.Richbound.Infrastructure.Utility;

/// <summary>
/// Provides utility methods and regular expressions for string manipulation and pattern matching.
/// </summary>
public static partial class RegexUtility
{
    [GeneratedRegex(@"\p{L}")]
    public static partial Regex UnicodeLetterRegex();

    [GeneratedRegex(@"^\d+$")]
    public static partial Regex OnlyNumbersRegex();
}
