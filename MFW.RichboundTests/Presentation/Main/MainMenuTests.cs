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
    public void Initialize()
    {
        _assemblyVersionProviderMock = new Mock<IAssemblyVersionProvider>(MockBehavior.Strict);
        _consoleLoggerMock = new Mock<IConsoleLogger>(MockBehavior.Strict);

        _sut = new MainMenu(_assemblyVersionProviderMock.Object, _consoleLoggerMock.Object);
    }

    [TestMethod]
    public void DisplayMainPrompt_ShouldOutputVersion()
    {
        // Arrange
        const string expected = " v1.2.3";

        const string input = "3\n";

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
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.Contains($"=== {DisplayText.AppTitle}{expected} ===", actualOutput);

        Assert.IsNull(actual);

        _assemblyVersionProviderMock.Verify();
    }

    [TestMethod]
    public void DisplayMainPrompt_WithNullVersion_ShouldNotOutputVersion()
    {
        // Arrange
        const string input = "3\n";

        _assemblyVersionProviderMock
            .Setup(x => x.GetVersion())
            .Returns((Version?)null)
            .Verifiable(Times.Once);

        _consoleLoggerMock
            .Setup(x => x.LogWarning("Unable to retrieve application version.", Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);
        var consoleOutput = new StringWriter();

        Console.SetIn(consoleInput);
        Console.SetOut(consoleOutput);

        // Act
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.Contains($"=== {DisplayText.AppTitle} ===", actualOutput);

        Assert.IsNull(actual);

        _assemblyVersionProviderMock.Verify();
        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    [DataRow(PromptType.NewGame, "1\n")]
    [DataRow(PromptType.LoadGame, "2\n")]
    [DataRow(null, "3\n")]
    public void DisplayMainPrompt_ShouldReturnPromptType(PromptType? expected, string input)
    {
        // Arrange
        var version = new Version(1, 2, 3);

        _assemblyVersionProviderMock
            .Setup(x => x.GetVersion())
            .Returns(version)
            .Verifiable(Times.Once);

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);

        _assemblyVersionProviderMock.Verify();
    }

    [TestMethod]
    [DataRow("\n3\n")]
    [DataRow("4\n3\n")]
    [DataRow("0\n3\n")]
    [DataRow("-1\n3\n")]
    [DataRow("TEST\n3\n")]
    public void DisplayMainPrompt_WithInvalidInput_ShouldOutputError(string input)
    {
        // Arrange
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
        var actual = _sut.DisplayMainPrompt();
        var actualOutput = consoleOutput.ToString();

        // Assert
        Assert.Contains("Please select a valid menu option.", actualOutput);
        Assert.IsNull(actual);

        _assemblyVersionProviderMock.Verify();
    }
}
