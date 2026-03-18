using MFW.Richbound.Infrastructure.Utility;

namespace MFW.RichboundTests.Infrastructure.Utility;

[TestClass]
public class RegexUtilityTests
{
    [TestMethod]
    [DataRow("a")]
    [DataRow("A")]
    [DataRow("á")]
    [DataRow("ß")]
    [DataRow("Match1")]
    public void UnicodeLetterRegex_ShouldMatch(string input)
    {
        // Act
        var actualMatch = RegexUtility.UnicodeLetterRegex().IsMatch(input);

        // Assert
        Assert.IsTrue(actualMatch);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("123")]
    [DataRow("!@#")]
    public void UnicodeLetterRegex_WithoutLetters_ShouldNotMatch(string input)
    {
        // Act
        var actualMatch = RegexUtility.UnicodeLetterRegex().IsMatch(input);

        // Assert
        Assert.IsFalse(actualMatch);
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("123")]
    public void OnlyNumbersRegex_ShouldMatch(string input)
    {
        // Act
        var actualMatch = RegexUtility.OnlyNumbersRegex().IsMatch(input);

        // Assert
        Assert.IsTrue(actualMatch);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("12a3")]
    [DataRow("12 3")]
    [DataRow("-123")]
    [DataRow("123.45")]
    public void OnlyNumbersRegex_WithoutDigits_ShouldNotMatch(string input)
    {
        // Act
        var actualMatch = RegexUtility.OnlyNumbersRegex().IsMatch(input);

        // Assert
        Assert.IsFalse(actualMatch);
    }
}
