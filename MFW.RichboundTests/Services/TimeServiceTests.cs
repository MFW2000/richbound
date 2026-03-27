using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Services;
using Moq;

namespace MFW.RichboundTests.Services;

[TestClass]
public class TimeServiceTests
{
    private Mock<IGameState> _gameStateMock = null!;
    private Mock<IConsoleLogger> _consoleLoggerMock = null!;

    private TimeService _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);
        _consoleLoggerMock = new Mock<IConsoleLogger>(MockBehavior.Strict);

        _sut = new TimeService(_gameStateMock.Object, _consoleLoggerMock.Object);
    }

    [TestMethod]
    public void PassTime_ShouldUpdateTime()
    {
        // Arrange
        const int hoursPassed = 1;

        _consoleLoggerMock
            .Setup(x => x.LogDebug(It.IsAny<string>(), It.IsAny<string>()))
            .Verifiable(Times.Once);

        _gameStateMock
            .SetupGet(x => x.Time)
            .Returns(0)
            .Verifiable(Times.Once);
        _gameStateMock
            .Setup(x => x.UpdateTime(hoursPassed))
            .Verifiable(Times.Once);

        // Act
        _sut.PassTime(hoursPassed);

        // Assert
        _consoleLoggerMock.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    public void PassTime_WithNewDay_ShouldHandleDailyUpdates()
    {
        // Arrange
        const int hoursPassed = 5;

        _consoleLoggerMock
            .Setup(x => x.LogDebug(It.IsAny<string>(), It.IsAny<string>()))
            .Verifiable(Times.Exactly(2));

        _gameStateMock
            .SetupGet(x => x.Time)
            .Returns(20)
            .Verifiable(Times.Once);
        _gameStateMock
            .Setup(x => x.UpdateDay())
            .Verifiable(Times.Once);
        _gameStateMock
            .Setup(x => x.UpdateTime(hoursPassed))
            .Verifiable(Times.Once);

        // Act
        _sut.PassTime(hoursPassed);

        // Assert
        _consoleLoggerMock.Verify();
        _gameStateMock.Verify();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(25)]
    public void PassTime_WithInvalidHoursPassed_ShouldThrowArgumentOutOfRangeException(int hoursPassed)
    {
        // Act & Assert
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => _sut.PassTime(hoursPassed));
    }
}
