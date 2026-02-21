using MFW.Richbound.Domain;

namespace MFW.RichboundTests.Domain;

[TestClass]
public class GameStateTests
{
    private GameState _sut = null!;

    [TestMethod]
    [DataRow("John")]
    [DataRow(" John ")]
    public void FirstName_ReturnsTrimmedValue(string value)
    {
        // Arrange
        const string expected = "John";

        _sut = new GameState
        {
            FirstName = value,
            LastName = "Doe",
            Health = 100,
            Hunger = 100,
            Thirst = 100,
            PocketMoney = 100,
            BankBalance = 100
        };

        // Act
        var actual = _sut.FirstName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void FirstName_WithInvalidValue_ThrowsArgumentException(string value)
    {
        // Arrange, Act, Assert
        Assert.ThrowsExactly<ArgumentException>(() =>
            new GameState
            {
                FirstName = value,
                LastName = "Doe",
                Health = 100,
                Hunger = 100,
                Thirst = 100,
                PocketMoney = 100,
                BankBalance = 100
            });
    }

    [TestMethod]
    [DataRow("Doe")]
    [DataRow(" Doe ")]
    public void LastName_ReturnsTrimmedValue(string value)
    {
        // Arrange
        const string expected = "Doe";

        _sut = new GameState
        {
            FirstName = "John",
            LastName = value,
            Health = 100,
            Hunger = 100,
            Thirst = 100,
            PocketMoney = 100,
            BankBalance = 100
        };

        // Act
        var actual = _sut.LastName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void LastName_WithInvalidValue_ThrowsArgumentException(string value)
    {
        // Arrange, Act, Assert
        Assert.ThrowsExactly<ArgumentException>(() =>
            new GameState
            {
                FirstName = "John",
                LastName = value,
                Health = 100,
                Hunger = 100,
                Thirst = 100,
                PocketMoney = 100,
                BankBalance = 100
            });
    }
}
