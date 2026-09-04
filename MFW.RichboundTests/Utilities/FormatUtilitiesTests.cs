using MFW.Richbound.Enumerations;
using MFW.Richbound.Utilities;

namespace MFW.RichboundTests.Utilities;

[TestClass]
public class FormatUtilitiesTests
{
    [TestMethod]
    [DataRow("09:00", 9)]
    [DataRow("10:00", 10)]
    public void Time_ReturnsFormattedTime(string expectedTimeString, int value)
    {
        // Act
        var actualTimeString = FormatUtilities.Time(value);

        // Assert
        Assert.AreEqual(expectedTimeString, actualTimeString);
    }

    [TestMethod]
    [DataRow("Mr.", Gender.Male)]
    [DataRow("Ms.", Gender.Female)]
    public void Title_ReturnsFormattedTitle(string expectedTitle, Gender gender)
    {
        // Act
        var actualTitle = FormatUtilities.Title(gender);

        // Assert
        Assert.AreEqual(expectedTitle, actualTitle);
    }

    [TestMethod]
    public void Currency_ReturnsFormattedCurrency()
    {
        // Arrange
        const string expectedCurrency = "$10";
        const int value = 10;

        // Act
        var actualCurrency = FormatUtilities.Currency(value);

        // Assert
        Assert.AreEqual(expectedCurrency, actualCurrency);
    }

    [TestMethod]
    public void Percentage()
    {
        // Arrange
        const string expectedPercentage = "10%";
        const int value = 10;

        // Act
        var actualPercentage = FormatUtilities.Percentage(value);

        // Assert
        Assert.AreEqual(expectedPercentage, actualPercentage);
    }

    [TestMethod]
    public void HitPoints()
    {
        // Arrange
        const string expectedHitPoints = "10 HP";
        const int value = 10;

        // Act
        var actualHitPoints = FormatUtilities.HitPoints(value);

        // Assert
        Assert.AreEqual(expectedHitPoints, actualHitPoints);
    }
}
