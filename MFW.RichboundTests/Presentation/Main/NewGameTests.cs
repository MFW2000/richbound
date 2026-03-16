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
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(PromptType.NewGameIntro, actual);
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
        const PromptType expected = PromptType.MainMenu;

        _saveFileManager
            .Setup(x => x.HasSaveFile())
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
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(PromptType.MainMenu, actual);
        Assert.Contains("A save file already exists.", actualOutput);
        Assert.Contains("Do you wish to overwrite it [y/N]:", actualOutput);
        Assert.Contains("Please enter 'yes' (y) or 'no' (n).", actualOutput);

        _saveFileManager.Verify();
    }

    [TestMethod]
    [DataRow("M\nDoe\nJ\nY\n\n", Gender.Male)]
    [DataRow("Male\nDoe\nJ\nY\n\n", Gender.Male)]
    [DataRow("F\nDoe\nJ\nY\n\n", Gender.Female)]
    [DataRow("Female\nDoe\nJ\nY\n\n", Gender.Female)]
    public void DisplayMainPrompt_PromptGenderShouldReturnGender(string input, Gender expectedGender)
    {
        // Arrange
        var gameStateDto = TestHelper.GetGameStateDto(gender: expectedGender, lastName: "Doe", firstName: "J");

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
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(PromptType.NewGameIntro, actual);
        Assert.Contains("Choose your gender (male/female) [m/f]:", actualOutput);
        Assert.Contains("Your character has been created successfully.", actualOutput);

        _saveFileManager.Verify();
        _gameStateMock.Verify();
    }
}
