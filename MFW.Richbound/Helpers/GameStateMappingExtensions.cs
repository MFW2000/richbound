using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Models;

namespace MFW.Richbound.Helpers;

/// <summary>
/// Provides extension methods for mapping <see cref="IGameState"/> objects to <see cref="GameStateDto"/> objects.
/// </summary>
public static class GameStateMappingExtensions
{
    /// <summary>
    /// Maps an <see cref="IGameState"/> object to a <see cref="GameStateDto"/> object.
    /// </summary>
    /// <param name="gameState">The <see cref="IGameState"/> object to map.</param>
    /// <returns>The mapped <see cref="GameStateDto"/> object.</returns>
    public static GameStateDto MapToGameStateDto(this IGameState gameState)
    {
        return new GameStateDto(
            gameState.Gender,
            gameState.FirstName,
            gameState.LastName,
            gameState.Health,
            gameState.Hunger,
            gameState.Thirst,
            gameState.PocketMoney,
            gameState.BankBalance);
    }
}
