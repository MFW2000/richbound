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
    public void FullName_ReturnsFullName()
    {
        // Arrange
        const string expected = "John Doe";

        var state = TestHelper.GetGameStateDto();

        _sut.Initialize(state);

        // Act
        var actual = _sut.FullName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void FullName_EmptyName_ReturnsEmptyString()
    {
        // Arrange
        var expected = string.Empty;

        var state = TestHelper.GetGameStateDto(firstName: string.Empty, lastName: string.Empty);

        _sut.Initialize(state);

        // Act
        var actual = _sut.FullName;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Title_Male_ReturnsMr()
    {
        // Arrange
        const string expected = "Mr.";

        var state = TestHelper.GetGameStateDto();

        _sut.Initialize(state);

        // Act
        var actual = _sut.Title;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Title_Female_ReturnsMs()
    {
        // Arrange
        const string expected = "Ms.";

        var state = TestHelper.GetGameStateDto(gender: Gender.Female, firstName: "Jane");

        _sut.Initialize(state);

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
    public void UpdateHealth_UpdatesHealth(int current, int delta, int expected)
    {
        // Arrange
        var state = TestHelper.GetGameStateDto(health: current);

        _sut.Initialize(state);

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
    public void UpdateHunger_UpdatesHunger(int current, int delta, int expected)
    {
        // Arrange
        var state = TestHelper.GetGameStateDto(hunger: current);

        _sut.Initialize(state);

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
    public void UpdateThirst_UpdatesThirst(int current, int delta, int expected)
    {
        // Arrange
        var state = TestHelper.GetGameStateDto(thirst: current);

        _sut.Initialize(state);

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
    public void UpdatePocketMoney_UpdatesPocketMoney(double current, double delta, double expected)
    {
        // Arrange
        var state = TestHelper.GetGameStateDto(pocketMoney: current);

        _sut.Initialize(state);

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
    public void UpdateBankBalance_UpdatesBankBalance(double current, double delta, double expected)
    {
        // Arrange
        var state = TestHelper.GetGameStateDto(bankBalance: current);

        _sut.Initialize(state);

        // Act
        _sut.UpdateBankBalance(delta);

        var actual = _sut.BankBalance;

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
