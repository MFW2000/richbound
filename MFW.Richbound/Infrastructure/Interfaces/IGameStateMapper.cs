using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure.Interfaces;

/// <summary>
/// Defines a contract for mapping <see cref="IGameState"/> objects to <see cref="GameStateDto"/> objects.
/// </summary>
public interface IGameStateMapper
{
    /// <summary>
    /// Maps an <see cref="IGameState"/> object to a <see cref="GameStateDto"/> object.
    /// </summary>
    /// <param name="gameState">The <see cref="IGameState"/> object to map.</param>
    /// <returns>The mapped <see cref="GameStateDto"/> object.</returns>
    GameStateDto MapToDto(IGameState gameState);
}
