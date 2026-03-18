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
        const string expectedFullName = "John Doe";

        var gameStateDto = TestHelper.GetGameStateDto();

        _sut.Initialize(gameStateDto);

        // Act
        var actualFullName = _sut.FullName;

        // Assert
        Assert.AreEqual(expectedFullName, actualFullName);
    }

    [TestMethod]
    public void FullName_WithEmptyName_ShouldReturnEmptyString()
    {
        // Arrange
        var expectedFullName = string.Empty;

        var gameStateDto = TestHelper.GetGameStateDto(firstName: string.Empty, lastName: string.Empty);

        _sut.Initialize(gameStateDto);

        // Act
        var actualFullName = _sut.FullName;

        // Assert
        Assert.AreEqual(expectedFullName, actualFullName);
    }

    [TestMethod]
    public void Title_AsMale_ShouldReturnMr()
    {
        // Arrange
        const string expectedTitle = "Mr.";

        var gameStateDto = TestHelper.GetGameStateDto();

        _sut.Initialize(gameStateDto);

        // Act
        var actualTitle = _sut.Title;

        // Assert
        Assert.AreEqual(expectedTitle, actualTitle);
    }

    [TestMethod]
    public void Title_AsFemale_ShouldReturnMs()
    {
        // Arrange
        const string expectedTitle = "Ms.";

        var gameStateDto = TestHelper.GetGameStateDto(gender: Gender.Female);

        _sut.Initialize(gameStateDto);

        // Act
        var actualTitle = _sut.Title;

        // Assert
        Assert.AreEqual(expectedTitle, actualTitle);
    }

    [TestMethod]
    [DataRow(90, 100, -10)]
    [DataRow(100, 90, 10)]
    [DataRow(100, 100, 10)]
    [DataRow(0, 0, -10)]
    public void UpdateHealth_ShouldUpdateHealth(int expectedHealth, int current, int delta)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(health: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateHealth(delta);

        var actualHealth = _sut.Health;

        // Assert
        Assert.AreEqual(expectedHealth, actualHealth);
    }

    [TestMethod]
    [DataRow(90, 100, -10)]
    [DataRow(100, 90, 10)]
    [DataRow(100, 100, 10)]
    [DataRow(0, 0, -10)]
    public void UpdateHunger_ShouldUpdateHunger(int expectedHunger, int current, int delta)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(hunger: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateHunger(delta);

        var actualHunger = _sut.Hunger;

        // Assert
        Assert.AreEqual(expectedHunger, actualHunger);
    }

    [TestMethod]
    [DataRow(90, 100, -10)]
    [DataRow(100, 90, 10)]
    [DataRow(100, 100, 10)]
    [DataRow(0, 0, -10)]
    public void UpdateThirst_ShouldUpdateThirst(int expectedThirst, int current, int delta)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(thirst: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateThirst(delta);

        var actualThirst = _sut.Thirst;

        // Assert
        Assert.AreEqual(expectedThirst, actualThirst);
    }

    [TestMethod]
    [DataRow(10, 0, 10)]
    [DataRow(0, 10, -10)]
    [DataRow(0, 0, -10)]
    [DataRow(double.MaxValue, double.MaxValue, 10)]
    public void UpdatePocketMoney_ShouldUpdatePocketMoney(double expectedPocketMoney, double current, double delta)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(pocketMoney: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdatePocketMoney(delta);

        var actualPocketMoney = _sut.PocketMoney;

        // Assert
        Assert.AreEqual(expectedPocketMoney, actualPocketMoney);
    }

    [TestMethod]
    [DataRow(10, 0, 10)]
    [DataRow(0, 10, -10)]
    [DataRow(-10, 0, -10)]
    [DataRow(double.MaxValue, double.MaxValue, 10)]
    [DataRow(double.MinValue, double.MinValue, -10)]
    public void UpdateBankBalance_ShouldUpdateBankBalance(double expectedBankBalance, double current, double delta)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(bankBalance: current);

        _sut.Initialize(gameStateDto);

        // Act
        _sut.UpdateBankBalance(delta);

        var actualBankBalance = _sut.BankBalance;

        // Assert
        Assert.AreEqual(expectedBankBalance, actualBankBalance);
    }
}
