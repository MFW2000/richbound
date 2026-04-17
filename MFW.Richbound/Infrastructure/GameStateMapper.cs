using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure;

/// <summary>
/// Implements <see cref="IGameStateMapper"/> for mapping <see cref="IGameState"/> objects to
/// <see cref="GameStateDto"/> objects.
/// </summary>
public class GameStateMapper : IGameStateMapper
{
    /// <inheritdoc/>
    public GameStateDto MapToDto(IGameState gameState)
    {
        return new GameStateDto(
            gameState.Gender,
            gameState.FirstName,
            gameState.LastName,
            gameState.Health,
            gameState.Energy,
            gameState.Hunger,
            gameState.PocketMoney,
            gameState.BankBalance,
            gameState.Day,
            gameState.Time,
            gameState.LastLocation,
            gameState.HasUsedHomelessShelter);
    }
}
