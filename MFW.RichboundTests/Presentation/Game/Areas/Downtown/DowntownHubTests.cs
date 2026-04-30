using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Presentation.Game.Areas.Downtown;
using MFW.Richbound.Services.Interfaces;
using Moq;

namespace MFW.RichboundTests.Presentation.Game.Areas.Downtown;

[TestClass]
public class DowntownHubTests
{
    private Mock<IGameState> _gameStateMock = null!;
    private Mock<ICharacterService> _characterServiceMock = null!;

    private DowntownHub _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);
        _characterServiceMock = new Mock<ICharacterService>(MockBehavior.Strict);

        _sut = new DowntownHub(_gameStateMock.Object, _characterServiceMock.Object);
    }

    [TestMethod]
    [DataRow(PromptType.CharacterMenu, "1\n")]
    public void DisplayMainPrompt_ShouldReturnPromptType(PromptType expectedPromptType, string input)
    {
        // Arrange
        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
    }

    [TestMethod]
    [Ignore("Temporarily disabled for placeholder testing.")]
    [DataRow("\n1\n")]
    [DataRow("4\n1\n")]
    [DataRow("0\n1\n")]
    [DataRow("-1\n1\n")]
    [DataRow("TEST\n1\n")]
    public void DisplayMainPrompt_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange
        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(PromptType.CharacterMenu, actualPromptType);
        Assert.Contains("Please select a valid menu option.", actualOutput);
    }
}
