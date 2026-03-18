using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Models;
using MFW.Richbound.Presentation.Main;
using Moq;

namespace MFW.RichboundTests.Presentation.Main;

[TestClass]
public class LoadGameTests
{
    private Mock<ISaveFileManager> _saveFileManager = null!;
    private Mock<IGameState> _gameStateMock = null!;

    private LoadGame _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _saveFileManager = new Mock<ISaveFileManager>(MockBehavior.Strict);
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);

        _sut = new LoadGame(_saveFileManager.Object, _gameStateMock.Object);
    }

    [TestMethod]
    public void DisplayMainPrompt_ShouldLoadSaveGameAndReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(true)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.LoadGame())
            .Returns(gameStateDto)
            .Verifiable(Times.Once);
        _gameStateMock
            .Setup(x => x.Initialize(gameStateDto))
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Save game loaded successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_WithoutSaveFile_ShouldReturnMainMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        const string input = "\n";

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("No save game found. Returning to main menu.", actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_LoadFails_ShouldReturnMainMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        const string input = "\n";

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(true)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.LoadGame())
            .Returns((GameStateDto?)null)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Something went wrong while loading the save game. Returning to main menu.", actualOutput);

        _saveFileManager.Verify();
    }
}
