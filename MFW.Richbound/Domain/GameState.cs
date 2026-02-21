using MFW.Richbound.Domain.Interface;

namespace MFW.Richbound.Domain;

public class GameState : IGameState
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public int Health { get; private set; }

    public int Hunger { get; private set; }

    public int Thirst { get; private set; }

    public double PocketMoney { get; private set; }

    public double BankBalance { get; private set; }

    public void UpdateHealth(int delta)
    {
        Health = Math.Clamp(Health + delta, 0, 100);
    }

    public void UpdateHunger(int delta)
    {
        Hunger = Math.Clamp(Hunger + delta, 0, 100);
    }

    public void UpdateThirst(int delta)
    {
        Thirst = Math.Clamp(Thirst + delta, 0, 100);
    }

    public void UpdatePocketMoney(double delta)
    {
        PocketMoney = Math.Clamp(PocketMoney + delta, 0, double.MaxValue);
    }

    public void UpdateBankBalance(double delta)
    {
        BankBalance = Math.Clamp(BankBalance + delta, double.MinValue, double.MaxValue);
    }
}
