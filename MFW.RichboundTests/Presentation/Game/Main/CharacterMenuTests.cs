using MFW.Richbound;
using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation.Game.Main;
using Moq;

namespace MFW.RichboundTests.Presentation.Game.Main;

[TestClass]
public class CharacterMenuTests
{
    private Mock<ISaveFileManager> _saveFileManagerMock = null!;
    private Mock<IGameState> _gameStateMock = null!;
    private Mock<IGameStateMapper> _gameStateMapperMock = null!;
    private Mock<IConsoleLogger> _consoleLoggerMock = null!;

    private CharacterMenu _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _saveFileManagerMock = new Mock<ISaveFileManager>(MockBehavior.Strict);
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);
        _gameStateMapperMock = new Mock<IGameStateMapper>(MockBehavior.Strict);
        _consoleLoggerMock = new Mock<IConsoleLogger>(MockBehavior.Strict);

        _sut = new CharacterMenu(
            _saveFileManagerMock.Object,
            _gameStateMock.Object,
            _gameStateMapperMock.Object,
            _consoleLoggerMock.Object);
    }

    [TestMethod]
    public void DisplayMainPrompt_ShouldOutputStats()
    {
        // Arrange
        const string expectedFullName = "John Doe";

        const string input = "5\nno\n";

        _gameStateMock
            .SetupGet(x => x.FullName)
            .Returns(expectedFullName)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.Contains(expectedFullName, actualOutput);

        Assert.IsNull(actual);

        _gameStateMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_CloseSelected_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "1\n";

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DisplayMainPrompt_InventorySelected_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "2\n";

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DisplayMainPrompt_SaveGameSelected_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "3\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_SaveGameSelected_SaveFails_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "3\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(false)
            .Verifiable(Times.Once);

        _consoleLoggerMock
            .Setup(x => x.LogError($"Failed to save game from {nameof(CharacterMenu)}.", Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_MainMenuSelectedWithoutSave_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.MainMenu;

        const string input = "4\nno\n";

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DisplayMainPrompt_MainMenuSelectedWithSave_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.MainMenu;

        const string input = "4\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_MainMenuSelectedWithSave_SaveFails_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "4\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(false)
            .Verifiable(Times.Once);

        _consoleLoggerMock
            .Setup(x => x.LogError($"Failed to save game from {nameof(CharacterMenu)}.", Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_ExitGameSelectedWithoutSave_ShouldReturnNull()
    {
        // Arrange
        PromptType? expected = null;

        const string input = "5\nno\n";

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DisplayMainPrompt_ExitGameSelectedWithSave_ShouldReturnNull()
    {
        // Arrange
        PromptType? expected = null;

        const string input = "5\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_ExitGameSelectedWithSave_SaveFails_ShouldReturnPromptType()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "5\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.FullName).Returns("John Doe");

        _gameStateMapperMock
            .Setup(x => x.MapToDto(_gameStateMock.Object))
            .Returns(gameStateDto)
            .Verifiable(Times.Once);

        _saveFileManagerMock
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(false)
            .Verifiable(Times.Once);

        _consoleLoggerMock
            .Setup(x => x.LogError($"Failed to save game from {nameof(CharacterMenu)}.", Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }
}
