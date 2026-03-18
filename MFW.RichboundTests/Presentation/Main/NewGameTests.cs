using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation.Main;
using Moq;

namespace MFW.RichboundTests.Presentation.Main;

[TestClass]
public class NewGameTests
{
    private Mock<ISaveFileManager> _saveFileManager = null!;
    private Mock<IGameState> _gameStateMock = null!;

    private NewGame _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _saveFileManager = new Mock<ISaveFileManager>(MockBehavior.Strict);
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);

        _sut = new NewGame(_saveFileManager.Object, _gameStateMock.Object);
    }

    [TestMethod]
    [DataRow("Y\nM\nDoe\nJohn\nY\n\n")]
    [DataRow("Yes\nM\nDoe\nJohn\nY\n\n")]
    public void DisplayMainPrompt_PromptOverwriteSave_WithOverwrite_ShouldContinue(string input)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto();

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(true)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
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
        Assert.AreEqual(PromptType.NewGameIntro, actualPromptType);
        Assert.Contains("A save file already exists.", actualOutput);
        Assert.Contains("Do you wish to overwrite it [y/N]:", actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    [DataRow("\n")]
    [DataRow("N\n")]
    [DataRow("No\n")]
    public void DisplayMainPrompt_PromptOverwriteSave_WithoutOverwrite_ShouldReturnMainMenu(string input)
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        _saveFileManager
            .Setup(x => x.HasSaveFile())
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
        Assert.Contains("A save file already exists.", actualOutput);
        Assert.Contains("Do you wish to overwrite it [y/N]:", actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow("A\nN\n")]
    [DataRow("1\nN\n")]
    public void DisplayMainPrompt_PromptOverwriteSave_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange
        _saveFileManager
            .Setup(x => x.HasSaveFile())
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
        Assert.AreEqual(PromptType.MainMenu, actualPromptType);
        Assert.Contains("A save file already exists.", actualOutput);
        Assert.Contains("Do you wish to overwrite it [y/N]:", actualOutput);
        Assert.Contains("Please enter 'yes' (y) or 'no' (n).", actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow(Gender.Male, "John", "M\nDoe\nJohn\nY\n\n")]
    [DataRow(Gender.Male, "John", "Male\nDoe\nJohn\nY\n\n")]
    [DataRow(Gender.Female, "Jane", "F\nDoe\nJane\nY\n\n")]
    [DataRow(Gender.Female, "Jane", "Female\nDoe\nJane\nY\n\n")]
    public void DisplayMainPrompt_PromptGender_ShouldReturnGender(Gender expectedGender, string firstName, string input)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(gender: expectedGender, firstName: firstName);

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
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
        Assert.AreEqual(PromptType.NewGameIntro, actualPromptType);
        Assert.Contains("Choose your gender (male/female) [m/f]:", actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    [DataRow("Please enter 'male' (m) or 'female' (f).", "1\nM\nDoe\nJohn\nN\n")]
    [DataRow("Please enter 'male' (m) or 'female' (f).", "A\nM\nDoe\nJohn\nN\n")]
    [DataRow("You must enter a gender.", " \nM\nDoe\nJohn\nN\n")]
    public void DisplayMainPrompt_PromptGender_WithInvalidInput_ShouldOutputError(string expectedError, string input)
    {
        // Arrange
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
        Assert.AreEqual(PromptType.NewGame, actualPromptType);
        Assert.Contains("Choose your gender (male/female) [m/f]:", actualOutput);
        Assert.Contains(expectedError, actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow("Doe", "M\nDoe\nJohn\nY\n\n")]
    [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAA", "M\nAAAAAAAAAAAAAAAAAAAAAAAAA\nJohn\nY\n\n")]
    public void DisplayMainPrompt_PromptLastName_ShouldReturnLastName(string expectedLastName, string input)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(lastName: expectedLastName);

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
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
        Assert.AreEqual(PromptType.NewGameIntro, actualPromptType);
        Assert.Contains("What is your last name (max 25 characters):", actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    [DataRow("Your character must have a last name.", "M\n \nDoe\nJohn\nN\n")]
    [DataRow("Your character's last name cannot be longer than 25 characters.", "M\nAAAAAAAAAAAAAAAAAAAAAAAAAA\nDoe\nJohn\nN\n")]
    [DataRow("Your character's last name must at least contain one letter.", "M\n123\nDoe\nJohn\nN\n")]
    public void DisplayMainPrompt_PromptLastName_WithInvalidInput_ShouldOutputError(string expectedError, string input)
    {
        // Arrange
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
        Assert.AreEqual(PromptType.NewGame, actualPromptType);
        Assert.Contains("What is your last name (max 25 characters):", actualOutput);
        Assert.Contains(expectedError, actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow("John", "M\nDoe\nJohn\nY\n\n")]
    [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAA", "M\nDoe\nAAAAAAAAAAAAAAAAAAAAAAAAA\nY\n\n")]
    public void DisplayMainPrompt_PromptFirstName_ShouldReturnLastName(string expectedFirstName, string input)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(firstName: expectedFirstName);

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
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
        Assert.AreEqual(PromptType.NewGameIntro, actualPromptType);
        Assert.Contains("What is your first name (max 25 characters):", actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    [DataRow("Your character must have a first name.", "M\nDoe\n \nJohn\nN\n")]
    [DataRow("Your character's first name cannot be longer than 25 characters.", "M\nDoe\nAAAAAAAAAAAAAAAAAAAAAAAAAA\nJohn\nN\n")]
    [DataRow("Your character's first name must at least contain one letter.", "M\nDoe\n123\nJohn\nN\n")]
    public void DisplayMainPrompt_PromptFirstName_WithInvalidInput_ShouldOutputError(string expectedError, string input)
    {
        // Arrange
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
        Assert.AreEqual(PromptType.NewGame, actualPromptType);
        Assert.Contains("What is your first name (max 25 characters):", actualOutput);
        Assert.Contains(expectedError, actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow("You are Mr. John Doe. Is this correct? [y/n]:", Gender.Male, "John", "M\nDoe\nJohn\nY\n\n")]
    [DataRow("You are Ms. Jane Doe. Is this correct? [y/n]:", Gender.Female, "Jane", "F\nDoe\nJane\nY\n\n")]
    public void DisplayMainPrompt_PromptCharacterConfirmation_ShouldReturnNewGameIntro(
        string expectedConfirmation,
        Gender gender,
        string firstName,
        string input)
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.NewGameIntro;

        var gameStateDto = TestHelper.GetGameStateDto(gender: gender, firstName: firstName);

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
            .Returns(true)
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
        Assert.Contains(expectedConfirmation, actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_PromptCharacterConfirmation_WithCancelInput_ShouldReturnNewGame()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.NewGame;

        const string input = "M\nDoe\nJohn\nN\n";

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actualPromptType = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expectedPromptType, actualPromptType);

        _saveFileManager.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_SaveFails_ShouldReturnMainMenu()
    {
        // Arrange
        const PromptType expectedPromptType = PromptType.MainMenu;

        const string input = "M\nDoe\nJohn\nY\n\n";

        var gameStateDto = TestHelper.GetGameStateDto();

        _saveFileManager
            .Setup(x => x.HasSaveFile())
            .Returns(false)
            .Verifiable(Times.Once);
        _saveFileManager
            .Setup(x => x.SaveGame(gameStateDto))
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
        Assert.Contains("Something went wrong while saving your new character. Returning to main menu.", actualOutput);

        _saveFileManager.Verify();
    }
}
