using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Services;

/// <summary>
/// Implements <see cref="ITimeService"/> for handling time-related operations.
/// </summary>
public class TimeService(IGameState gameState) : ITimeService
{
    /// <inheritdoc/>
    public void PassTime(int hoursPassed)
    {
        if (hoursPassed is <= 0 or > 24)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hoursPassed),
                hoursPassed,
                "Hours passed must be greater than 0 and less than or equal to 24.");
        }

        if (gameState.Time + hoursPassed >= 24)
        {
            gameState.UpdateDay();

            HandleDailyUpdates();
        }

        gameState.UpdateTime(hoursPassed);
    }

    /// <summary>
    /// Handles any daily updates that need to be performed.
    /// </summary>
    private void HandleDailyUpdates()
    {
        gameState.HasUsedHomelessShelter = false;
    }
}
