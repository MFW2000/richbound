using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Services.Interfaces;

public interface ICharacterService
{
    bool HandleActivity(int hoursToComplete, int energyToComplete);

    bool HandleSleep(BedQuality bedQuality);

    bool HandleHunger(int hungerRating);

    bool HandleHealthRestored();

    bool HandleDamageTaken();
}
