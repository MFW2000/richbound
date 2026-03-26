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

        const string input = "5\nN\n";

        _gameStateMock
            .SetupGet(x => x.Day)
            .Returns(1)
            .Verifiable(Times.Once);
        _gameStateMock
            .SetupGet(x => x.TimeText)
            .Returns("06:00")
            .Verifiable(Times.Once);
        _gameStateMock
            .SetupGet(x => x.FullName)
            .Returns(expectedFullName)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.IsNull(actualPromptType);
        Assert.Contains(expectedFullName, actualOutput);

        _gameStateMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_CloseSelected_ShouldReturnLastLocation()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.DowntownHub;

        const string input = "1\n";

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());
        _gameStateMock
            .SetupGet(x => x.LastLocation)
            .Returns(expectedPromptType)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);

        _gameStateMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_InventorySelected_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "2\n";

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
    }

    [TestMethod]
    public void DisplayMainPrompt_SaveGameSelected_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "3\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_SaveGameSelected_SaveFails_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "3\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    [DataRow("4\nN\n")]
    [DataRow("4\nNo\n")]
    public void DisplayMainPrompt_MainMenuSelectedWithoutSave_ShouldReturnMainMenu(string input)
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
    }

    [TestMethod]
    [DataRow("4\n\n")]
    [DataRow("4\nY\n")]
    [DataRow("4\nYes\n")]
    public void DisplayMainPrompt_MainMenuSelectedWithSave_ShouldReturnMainMenu(string input)
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_MainMenuSelectedWithSave_SaveFails_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "4\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    [DataRow("5\nN\n")]
    [DataRow("5\nNo\n")]
    public void DisplayMainPrompt_ExitGameSelectedWithoutSave_ShouldReturnNull(string input)
    {
        // Arrange
        PromptType? expectedPromptType = null;

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
    }

    [TestMethod]
    [DataRow("5\n\n")]
    [DataRow("5\nY\n")]
    [DataRow("5\nYes\n")]
    public void DisplayMainPrompt_ExitGameSelectedWithSave_ShouldReturnNull(string input)
    {
        // Arrange
        PromptType? expectedPromptType = null;

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Game saved successfully.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_ExitGameSelectedWithSave_SaveFails_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.CharacterMenu;

        const string input = "5\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

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
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);
        Assert.Contains("Something went wrong while saving your game, aborting.", actualOutput);

        _gameStateMapperMock.Verify();
        _saveFileManagerMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    [DataRow("\n5\nN\n")]
    [DataRow("6\n5\nN\n")]
    [DataRow("0\n5\nN\n")]
    [DataRow("-1\n5\nN\n")]
    [DataRow("Test\n5\nN\n")]
    public void DisplayMainPrompt_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange
        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.IsNull(actualPromptType);
        Assert.Contains("Please select a valid menu option.", actualOutput);
    }

    [TestMethod]
    [DataRow("\n5\nA\nN\n")]
    [DataRow("\n5\n1\nN\n")]
    public void DisplayMainPrompt_PromptLeaveGame_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange
        _gameStateMock.SetupGet(x => x.Day).Returns(It.IsAny<int>());
        _gameStateMock.SetupGet(x => x.TimeText).Returns(It.IsAny<string>());
        _gameStateMock.SetupGet(x => x.FullName).Returns(It.IsAny<string>());

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.IsNull(actualPromptType);
        Assert.Contains("Please enter 'yes' (y) or 'no' (n).", actualOutput);
    }
}
