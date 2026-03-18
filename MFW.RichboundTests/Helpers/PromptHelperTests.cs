using MFW.Richbound.Exceptions.Prompt;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Utility;

namespace MFW.RichboundTests.Helpers;

[TestClass]
public class PromptHelperTests
{
    [TestMethod]
    public void ReadString_WithAllowEmpty_ShouldReturnEmptyString()
    {
        // Arrange
        var expectedString = string.Empty;

        const string input = "\n";
        const bool allowEmpty = true;

        Console.SetIn(new StringReader(input));

        // Act
        var actualString = PromptHelper.ReadString(allowEmpty: allowEmpty);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void ReadString_WithoutAllowEmpty_ShouldThrowInputEmptyException()
    {
        // Arrange
        const string input = "\n";
        const bool allowEmpty = false;

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputEmptyException>(() => PromptHelper.ReadString(allowEmpty: allowEmpty));
    }

    [TestMethod]
    public void ReadString_WithTrim_ShouldReturnTrimmedString()
    {
        // Arrange
        const string expectedString = "untrimmed string";

        const string input = " untrimmed string \n";
        const bool trim = true;

        Console.SetIn(new StringReader(input));

        // Act
        var actualString = PromptHelper.ReadString(trim: trim);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void ReadString_WithoutTrim_ShouldReturnUntrimmedString()
    {
        // Arrange
        const string expectedString = " untrimmed string ";

        const string input = " untrimmed string \n";
        const bool trim = false;

        Console.SetIn(new StringReader(input));

        // Act
        var actualString = PromptHelper.ReadString(trim: trim);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void ReadString_WithinMaxLength_ShouldReturnString()
    {
        // Arrange
        const string expectedString = "12345";

        const string input = "12345\n";
        const int maxLength = 5;

        Console.SetIn(new StringReader(input));

        // Act
        var actualString = PromptHelper.ReadString(maxLength: maxLength);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void ReadString_OverMaxLength_ShouldThrowInputOutOfRangeException()
    {
        // Arrange
        const string input = "123456\n";
        const int maxLength = 5;

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputOutOfRangeException>(() => PromptHelper.ReadString(maxLength: maxLength));
    }

    [TestMethod]
    public void ReadString_MatchesRegex_ShouldReturnString()
    {
        // Arrange
        const string expectedString = "12345";

        const string input = "12345\n";

        Console.SetIn(new StringReader(input));

        // Act
        var actualString = PromptHelper.ReadString(matchRegex: RegexUtility.OnlyNumbersRegex());

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void ReadString_WithRegexMismatch_ShouldThrowInputRegexMismatchException()
    {
        // Arrange
        const string input = "mismatch\n";

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputRegexMismatchException>(() =>
            PromptHelper.ReadString(matchRegex: RegexUtility.OnlyNumbersRegex()));
    }

    [TestMethod]
    public void ReadInt_WithNoIntInput_ShouldThrowFormatException()
    {
        // Arrange
        const string input = "not an integer\n";

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<FormatException>(() => PromptHelper.ReadInt());
    }

    [TestMethod]
    public void ReadInt_WithAllowEmpty_ShouldReturnNull()
    {
        // Arrange
        const string input = "\n";
        const bool allowEmpty = true;

        Console.SetIn(new StringReader(input));

        // Act
        var actualInt = PromptHelper.ReadInt(allowEmpty: allowEmpty);

        // Assert
        Assert.IsNull(actualInt);
    }

    [TestMethod]
    public void ReadInt_WithoutAllowEmpty_ShouldThrowInputEmptyException()
    {
        // Arrange
        const string input = "\n";
        const bool allowEmpty = false;

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputEmptyException>(() => PromptHelper.ReadInt(allowEmpty: allowEmpty));
    }

    [TestMethod]
    public void ReadInt_WithinMinRange_ShouldReturnInt()
    {
        // Arrange
        const int expectedInt = 100;

        const string input = "100\n";
        const int minRange = 100;

        Console.SetIn(new StringReader(input));

        // Act
        var actualInt = PromptHelper.ReadInt(minRange: minRange);

        // Assert
        Assert.AreEqual(expectedInt, actualInt);
    }

    [TestMethod]
    public void ReadInt_OutOfMinRange_ShouldThrowInputOutOfRangeException()
    {
        // Arrange
        const string input = "99\n";
        const int minRange = 100;

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputOutOfRangeException>(() => PromptHelper.ReadInt(minRange: minRange));
    }

    [TestMethod]
    public void ReadInt_WithinMaxRange_ShouldReturnInt()
    {
        // Arrange
        const int expectedInt = 100;

        const string input = "100\n";
        const int maxRange = 100;

        Console.SetIn(new StringReader(input));

        // Act
        var actualInt = PromptHelper.ReadInt(maxRange: maxRange);

        // Assert
        Assert.AreEqual(expectedInt, actualInt);
    }

    [TestMethod]
    public void ReadInt_OutOfMaxRange_ShouldThrowInputOutOfRangeException()
    {
        // Arrange
        const string input = "101\n";
        const int maxRange = 100;

        Console.SetIn(new StringReader(input));

        // Act & Assert
        Assert.ThrowsExactly<InputOutOfRangeException>(() => PromptHelper.ReadInt(maxRange: maxRange));
    }
}
