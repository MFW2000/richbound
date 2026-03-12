using MFW.Richbound;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Presentation.Game;
using Moq;

namespace MFW.RichboundTests.Presentation.Game;

[TestClass]
public class GameIntroTests
{
    private Mock<IThreadWrapper> _threadWrapperMock = null!;

    private GameIntro _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _threadWrapperMock = new Mock<IThreadWrapper>(MockBehavior.Strict);

        _sut = new GameIntro(_threadWrapperMock.Object);
    }

    [TestMethod]
    public void DisplayMainPrompt_ShouldReturnCharacterMenu()
    {
        // Arrange
        const PromptType expected = PromptType.CharacterMenu;

        const string input = "\n";

        _threadWrapperMock
            .Setup(x => x.Sleep(Constants.DisplayDelayTimeMilliseconds))
            .Verifiable(Times.Exactly(6));

        var consoleInput = new StringReader(input);

        Console.SetIn(consoleInput);

        // Act
        var actual = _sut.DisplayMainPrompt();

        // Assert
        Assert.AreEqual(expected, actual);

        _threadWrapperMock.Verify();
    }
}
