using MFW.Richbound.Domain;
using MFW.Richbound.Enumerations;

namespace MFW.RichboundTests.Domain;

[TestClass]
public class GameStateTests
{
    private GameState _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _sut = new GameState();
    }

    [TestMethod]
    public void FullName_ShouldReturnFullName()
    {
        // Arrange
        const string expected = "John Doe";

        var gameStateDto = TestHelper.GetGameStateDto();

        _sut.Initialize(gameStateDto);

        // Act
        var actual = _sut.FullName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FullName_WithEmptyName_ShouldReturnEmptyString()
    {
        // Arrange
        var expected = string.Empty;

        var gameStateDto = TestHelper.GetGameStateDto(firstName: string.Empty, lastName: string.Empty);

        _sut.Initialize(gameStateDto);

        // Act
        var actual = _sut.FullName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Title_AsMale_ShouldReturnMr()
    {
        // Arrange
        const string expected = "Mr.";

        var gameStateDto = TestHelper.GetGameStateDto();

        _sut.Initialize(gameStateDto);

        // Act
        var actual = _sut.Title;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Title_AsFemale_ShouldReturnMs()
    {
        // Arrange
        const string expected = "Ms.";

        var gameStateDto = TestHelper.GetGameStateDto(gender: Gender.Female);

        _sut.Initialize(gameStateDto);

        // Act
        var actual = _sut.Title;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(100, -10, 90)]
    [DataRow(90, 10, 100)]
    [DataRow(100, 10, 100)]
    [DataRow(0, -10, 0)]
    public void UpdateHealth_ShouldUpdateHealth(int current, int delta, int expected)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(health: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateHealth(delta);

        var actual = _sut.Health;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(100, -10, 90)]
    [DataRow(90, 10, 100)]
    [DataRow(100, 10, 100)]
    [DataRow(0, -10, 0)]
    public void UpdateHunger_ShouldUpdateHunger(int current, int delta, int expected)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(hunger: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateHunger(delta);

        var actual = _sut.Hunger;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(100, -10, 90)]
    [DataRow(90, 10, 100)]
    [DataRow(100, 10, 100)]
    [DataRow(0, -10, 0)]
    public void UpdateThirst_ShouldUpdateThirst(int current, int delta, int expected)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(thirst: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateThirst(delta);

        var actual = _sut.Thirst;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(0, 10, 10)]
    [DataRow(10, -10, 0)]
    [DataRow(0, -10, 0)]
    [DataRow(double.MaxValue, 10, double.MaxValue)]
    public void UpdatePocketMoney_ShouldUpdatePocketMoney(double current, double delta, double expected)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(pocketMoney: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdatePocketMoney(delta);

        var actual = _sut.PocketMoney;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(0, 10, 10)]
    [DataRow(10, -10, 0)]
    [DataRow(0, -10, -10)]
    [DataRow(double.MaxValue, 10, double.MaxValue)]
    [DataRow(double.MinValue, -10, double.MinValue)]
    public void UpdateBankBalance_ShouldUpdateBankBalance(double current, double delta, double expected)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(bankBalance: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateBankBalance(delta);

        var actual = _sut.BankBalance;

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
