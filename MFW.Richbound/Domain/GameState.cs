using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Models;

namespace MFW.Richbound.Domain;

/// <summary>
/// Implements <see cref="IGameState"/> for representing the state of the game.
/// </summary>
public class GameState : IGameState
{
    private const int MinStatValue = 0;
    private const int MaxStatValue = 100;

    /// <inheritdoc/>
    public Gender Gender { get; private set; }

    /// <inheritdoc/>
    public string FirstName { get; private set; } = string.Empty;

    /// <inheritdoc/>
    public string LastName { get; private set; } = string.Empty;

    /// <inheritdoc/>
    public int Health { get; private set; }

    /// <inheritdoc/>
    public int Hunger { get; private set; }

    /// <inheritdoc/>
    public int Thirst { get; private set; }

    /// <inheritdoc/>
    public double PocketMoney { get; private set; }

    /// <inheritdoc/>
    public double BankBalance { get; private set; }

    /// <inheritdoc/>
    public PromptType LastLocation { get; private set; } = PromptType.DowntownHub;

    /// <inheritdoc/>
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <inheritdoc/>
    public string Title => Gender == Gender.Male ? "Mr." : "Ms.";

    /// <inheritdoc/>
    public void Initialize(GameStateDto gameStateDto)
    {
        Gender = gameStateDto.Gender;
        FirstName = gameStateDto.FirstName;
        LastName = gameStateDto.LastName;
        Health = gameStateDto.Health;
        Hunger = gameStateDto.Hunger;
        Thirst = gameStateDto.Thirst;
        PocketMoney = gameStateDto.PocketMoney;
        BankBalance = gameStateDto.BankBalance;
        LastLocation = gameStateDto.LastLocation;
    }

    /// <inheritdoc/>
    public void UpdateHealth(int delta)
    {
        Health = Math.Clamp(Health + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdateHunger(int delta)
    {
        Hunger = Math.Clamp(Hunger + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdateThirst(int delta)
    {
        Thirst = Math.Clamp(Thirst + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdatePocketMoney(double delta)
    {
        PocketMoney = Math.Clamp(PocketMoney + delta, 0, double.MaxValue);
    }

    /// <inheritdoc/>
    public void UpdateBankBalance(double delta)
    {
        BankBalance = Math.Clamp(BankBalance + delta, double.MinValue, double.MaxValue);
    }

    /// <inheritdoc/>
    public void UpdateLastLocation(LocationPromptType newLocation)
    {
        LastLocation = newLocation switch
        {
            LocationPromptType.DowntownHub => PromptType.DowntownHub,
            _ => LastLocation
        };
    }
}
