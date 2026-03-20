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
        int thirst = 100,
        double pocketMoney = 0,
        double bankBalance = 0,
        PromptType lastLocation = PromptType.DowntownHub)
    {
        return new GameStateDto(
            gender,
            firstName,
            lastName,
            health,
            hunger,
            thirst,
            pocketMoney,
            bankBalance,
            lastLocation);
    }
}
