using MFW.Richbound.Domain;
using MFW.Richbound.Infrastructure;

namespace MFW.RichboundTests.Infrastructure;

[TestClass]
public class GameStateMapperTests
{
    private GameStateMapper _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _sut = new GameStateMapper();
    }

    [TestMethod]
    public void MapToDto_ShouldMapToGameStateDto()
    {
        // Arrange
        var expectedGameStateDto = TestHelper.GetGameStateDto();

        var gameState = new GameState();

        gameState.Initialize(TestHelper.GetGameStateDto());

        // Act
        var actualGameStateDto = _sut.MapToDto(gameState);

        // Assert
        Assert.AreEqual(expectedGameStateDto, actualGameStateDto);
    }
}
