using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Services;

public class CharacterService(IGameState gameState, ITimeService timeService, IConsoleLogger logger) : ICharacterService
{
    private const int SleepDurationHours = 8;
    private const int BadBedQualitySleepEnergyRestoration = 50; // TODO: Test to make sure this is the minimum you need.
    private const int GoodBedQualitySleepEnergyRestoration = 100;

    public bool HandleActivity(int hoursToComplete, int energyToComplete)
    {
        if (hoursToComplete <= 0)
        {
            logger.LogWarning("A negative number of hours was passed to the activity handler.");

            throw new ArgumentOutOfRangeException(
                nameof(hoursToComplete),
                "Hours to complete must be a positive value.");
        }

        if (energyToComplete <= 0)
        {
            logger.LogWarning("A negative number of energy was passed to the activity handler.");

            throw new ArgumentOutOfRangeException(
                nameof(energyToComplete),
                "Energy to complete must be a positive value.");
        }

        var negativeEnergyDelta = -energyToComplete;

        if (gameState.Energy + negativeEnergyDelta <= Constants.MinCharacterStatValue)
        {
            return false;
        }

        timeService.PassTime(hoursToComplete);

        gameState.UpdateEnergy(negativeEnergyDelta);

        return true;
    }

    public bool HandleSleep(BedQuality bedQuality)
    {
        if (gameState.Energy + SleepDurationHours > Constants.MaxCharacterStatValue)
        {
            return false;
        }

        var energyRestored = bedQuality == BedQuality.Bad
            ? BadBedQualitySleepEnergyRestoration
            : GoodBedQualitySleepEnergyRestoration;

        timeService.PassTime(SleepDurationHours);

        gameState.UpdateEnergy(energyRestored);

        return true;
    }

    public bool HandleHunger(int hungerRating)
    {
        throw new NotImplementedException();
    }

    public bool HandleHealthRestored()
    {
        throw new NotImplementedException();
    }

    public bool HandleDamageTaken()
    {
        throw new NotImplementedException();
    }
}
