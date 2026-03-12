using MFW.Richbound.Domain;
using MFW.Richbound.Helpers;

namespace MFW.RichboundTests.Helpers;

[TestClass]
public class GameStateMappingExtensionsTests
{
    [TestMethod]
    public void MapToGameStateDto_MapsToGameStateDto()
    {
        // Arrange
        var expected = TestHelper.GetGameStateDto();

        var gameState = new GameState();

        gameState.Initialize(TestHelper.GetGameStateDto());

        // Act
        var actual = gameState.MapToGameStateDto();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
