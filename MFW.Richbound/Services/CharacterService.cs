using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Services;

/// <summary>
/// Implements <see cref="ICharacterService"/> for handling character mutation operations.
/// </summary>
public class CharacterService(IGameState gameState, ITimeService timeService) : ICharacterService
{
    /// <inheritdoc/>
    public bool HandleActivity(int hoursToComplete, int energyToComplete)
    {
        if (hoursToComplete <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hoursToComplete),
                hoursToComplete,
                "Hours to complete must be a positive value.");
        }

        if (energyToComplete <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(energyToComplete),
                energyToComplete,
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

    /// <inheritdoc/>
    public bool HandleSleep(Comfort comfortLevel)
    {
        if (gameState.Energy == Constants.MaxCharacterStatValue)
        {
            return false;
        }

        var energyRestored = comfortLevel switch
        {
            Comfort.Poor => Constants.ComfortLevelSleepPoor,
            Comfort.Average => Constants.ComfortLevelSleepAverage,
            Comfort.Good => Constants.ComfortLevelSleepGood,
            _ => throw new ArgumentOutOfRangeException(
                nameof(comfortLevel),
                comfortLevel,
                "Invalid comfort level was passed.")
        };

        timeService.PassTime(Constants.SleepDurationHours);

        gameState.UpdateEnergy(energyRestored);

        return true;
    }

    /// <inheritdoc/>
    public bool HandleEat(int foodPoints)
    {
        if (foodPoints is <= 0 or > Constants.MaxCharacterStatValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(foodPoints),
                foodPoints,
                "Food points must be positive and not greater than the maximum value.");
        }

        if (gameState.Hunger == Constants.MaxCharacterStatValue)
        {
            return false;
        }

        gameState.UpdateHunger(+foodPoints);

        return true;
    }

    /// <inheritdoc/>
    public bool HandleHealing(int hitPoints)
    {
        if (hitPoints is <= 0 or > Constants.MaxCharacterStatValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hitPoints),
                hitPoints,
                "Hit points must be positive and not greater than the maximum value.");
        }

        if (gameState.Health == Constants.MaxCharacterStatValue)
        {
            return false;
        }

        gameState.UpdateHealth(+hitPoints);

        return true;
    }

    /// <inheritdoc/>
    public void HandleDamage(int hitPoints)
    {
        if (hitPoints is <= 0 or > Constants.MaxCharacterStatValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(hitPoints),
                hitPoints,
                "Hit points must be positive and not greater than the maximum value.");
        }

        gameState.UpdateHealth(-hitPoints);
    }

    /// <inheritdoc/>
    public void HandleDeath(double hospitalBill)
    {
        gameState.UpdateBankBalance(-hospitalBill);
        gameState.UpdateTime(Constants.HospitalStayDurationHours);
        gameState.UpdateHealth(Constants.MaxCharacterStatValue);
        gameState.UpdateHunger(Constants.MaxCharacterStatValue);
        gameState.UpdateEnergy(Constants.MaxCharacterStatValue);
    }
}
