using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
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
    public void DisplayMainPrompt_WithNoSaveFile_ShouldReturnPromptType()
    {
        // Arrange

        // Act

        // Assert
    }
}
