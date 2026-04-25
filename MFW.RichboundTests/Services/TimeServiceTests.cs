using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Services;
using Moq;

namespace MFW.RichboundTests.Services;

[TestClass]
public class TimeServiceTests
{
    private Mock<IGameState> _gameStateMock = null!;

    private TimeService _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _gameStateMock = new Mock<IGameState>(MockBehavior.Strict);

        _sut = new TimeService(_gameStateMock.Object);
    }

    [TestMethod]
    public void PassTime_ShouldUpdateTime()
    {
        // Arrange
        const int hoursPassed = 1;

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
        _gameStateMock.Verify();
    }

    [TestMethod]
    public void PassTime_WithNewDay_ShouldHandleDailyUpdates()
    {
        // Arrange
        const int hoursPassed = 5;

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
        _gameStateMock
            .SetupSet(x => x.HasUsedHomelessShelter = false)
            .Verifiable(Times.Once);

        // Act
        _sut.PassTime(hoursPassed);

        // Assert
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
