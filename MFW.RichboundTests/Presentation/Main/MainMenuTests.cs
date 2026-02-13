using MFW.Richbound;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation.Main;
using MFW.Richbound.Providers.Interfaces;
using Moq;

namespace MFW.RichboundTests.Presentation.Main;

[TestClass]
public class MainMenuTests
{
    private Mock<IAssemblyVersionProvider> _assemblyVersionProviderMock = null!;
    private Mock<IConsoleLogger> _consoleLoggerMock = null!;

    private MainMenu _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _assemblyVersionProviderMock = new Mock<IAssemblyVersionProvider>(MockBehavior.Strict);
        _consoleLoggerMock = new Mock<IConsoleLogger>(MockBehavior.Strict);

        _sut = new MainMenu(_assemblyVersionProviderMock.Object, _consoleLoggerMock.Object);
    }

    [TestMethod]
    public void DisplayMainPrompt_ShouldOutputMenuAndVersion()
    {
        // Arrange
        const string input = "3\n";
        const string expectedVersionFormat = " v1.2.3";

        var version = new Version(1, 2, 3);

        _assemblyVersionProviderMock
            .Setup(x => x.GetVersion())
            .Returns(version)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        _sut.DisplayMainPrompt();

        var output = consoleOutput.ToString();

        // Assert
        Assert.Contains($"=== {DisplayText.AppTitle}{expectedVersionFormat} ===", output);
        Assert.Contains(DisplayText.AppSubtitle, output);
        Assert.Contains("--- Main Menu ---", output);
        Assert.Contains("1. New Game", output);
        Assert.Contains("2. Load Game", output);
        Assert.Contains("3. Exit", output);
        Assert.Contains(DisplayText.TooltipOption, output);

        _assemblyVersionProviderMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_WithNullVersion_ShouldNotOutputVersion()
    {
        // Arrange

        // Act

        // Assert
    }

    [TestMethod]
    [DataRow(PromptType.NewGame, "1\n")]
    [DataRow(PromptType.LoadGame, "2\n")]
    public void DisplayMainPrompt_ShouldReturnCorrectPrompt(PromptType expected, string input)
    {
        // Arrange

        // Act

        // Assert
    }

    [TestMethod]
    public void DisplayMainPrompt_WithExitInput_ShouldReturnNull()
    {
        // Arrange

        // Act

        // Assert
    }

    // After the invalid input, the test needs valid input to exit.
    [TestMethod]
    [DataRow("\n1\n")]
    [DataRow("4\n1\n")]
    [DataRow("0\n1\n")]
    [DataRow("-1\n1\n")]
    [DataRow("test\n1\n")]
    public void DisplayMainPrompt_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange

        // Act

        // Assert
    }
}
