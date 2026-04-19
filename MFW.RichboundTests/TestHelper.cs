using MFW.Richbound.Enumerations;
using MFW.Richbound.Models;

namespace MFW.RichboundTests;

public static class TestHelper
{
    public static GameStateDto GetGameStateDto(
        Gender gender = Gender.Male,
        string firstName = "John",
        string lastName = "Doe",
        int health = 100,
        int hunger = 100,
        int energy = 100,
        double pocketMoney = 0,
        double bankBalance = 0,
        int day = 1,
        int time = 6,
        PromptType lastLocation = PromptType.DowntownHub)
    {
        return new GameStateDto(
            Gender: gender,
            FirstName: firstName,
            LastName: lastName,
            Health: health,
            Hunger: hunger,
            Energy: energy,
            PocketMoney: pocketMoney,
            BankBalance: bankBalance,
            Day: day,
            Time: time,
            LastLocation: lastLocation);
    }
}
