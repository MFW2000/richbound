using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Services.Interfaces;

// TODO: Add documentation and tests.

/// <summary>
/// Defines a contract for handling character mutation operations.
/// </summary>
public interface ICharacterService
{
    bool HandleActivity(int hoursToComplete, int energyToComplete);

    bool HandleSleep(Comfort comfortLevel);

    bool HandleEat(int foodPoints);

    bool HandleHealing(int hitPoints);

    void HandleDamage(int hitPoints);

    void HandleDeath(double hospitalBill);
}
