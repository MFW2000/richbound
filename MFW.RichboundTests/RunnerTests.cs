using MFW.Richbound;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Factories.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation;
using Moq;

namespace MFW.RichboundTests;

[TestClass]
public class RunnerTests
{
    private Mock<IPromptFactory> _promptFactoryMock = null!;
    private Mock<IConsoleWrapper> _consoleWrapperMock = null!;
    private Mock<MainMenu> _mainMenuMock = null!;

    private Runner _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _promptFactoryMock = new Mock<IPromptFactory>(MockBehavior.Strict);
        _consoleWrapperMock = new Mock<IConsoleWrapper>(MockBehavior.Strict);
        _mainMenuMock = new Mock<MainMenu>(MockBehavior.Strict);

        _sut = new Runner(_promptFactoryMock.Object, _consoleWrapperMock.Object);
    }

    [TestMethod]
    public void Run_NavigatesToExit()
    {
        // Arrange
        _mainMenuMock
            .Setup(x => x.DisplayMainPrompt())
            .Returns((PromptType?)null)
            .Verifiable(Times.Once);

        _promptFactoryMock
            .Setup(x => x.CreatePrompt<MainMenu>())
            .Returns(_mainMenuMock.Object)
            .Verifiable(Times.Once);

        _consoleWrapperMock
            .Setup(x => x.Clear())
            .Verifiable(Times.Once);

        // Act
        _sut.Run();

        // Assert
        _mainMenuMock.Verify();
        _promptFactoryMock.Verify();
        _consoleWrapperMock.Verify();
    }
}
