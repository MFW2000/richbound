using MFW.Richbound.Domain.Interface;

namespace MFW.Richbound.Domain;

/// <summary>
/// Implements <see cref="IGameState"/> for representing the state of the game.
/// </summary>
public class GameState : IGameState
{
    private const int MinStatValue = 0;
    private const int MaxStatValue = 100;

    private int _health;
    private int _hunger;
    private int _thirst;
    private double _pocketMoney;
    private double _bankBalance;

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is empty or whitespace.</exception>
    public required string FirstName
    {
        get;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(value));
            }

            field = value.Trim();
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is empty or whitespace.</exception>
    public required string LastName
    {
        get;
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(value));
            }

            field = value.Trim();
        }
    }

    /// <inheritdoc/>
    public required int Health
    {
        get => _health;
        init => _health = Math.Clamp(value, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public required int Hunger
    {
        get => _hunger;
        init => _hunger = Math.Clamp(value, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public required int Thirst
    {
        get => _thirst;
        init => _thirst = Math.Clamp(value, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public required double PocketMoney
    {
        get => _pocketMoney;
        init => _pocketMoney = Math.Clamp(value, 0, double.MaxValue);
    }

    /// <inheritdoc/>
    public required double BankBalance
    {
        get => _bankBalance;
        init => _bankBalance = Math.Clamp(value, double.MinValue, double.MaxValue);
    }

    /// <inheritdoc/>
    public void UpdateHealth(int delta)
    {
        _health = Math.Clamp(Health + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdateHunger(int delta)
    {
        _hunger = Math.Clamp(Hunger + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdateThirst(int delta)
    {
        _thirst = Math.Clamp(Thirst + delta, MinStatValue, MaxStatValue);
    }

    /// <inheritdoc/>
    public void UpdatePocketMoney(double delta)
    {
        _pocketMoney = Math.Clamp(PocketMoney + delta, 0, double.MaxValue);
    }

    /// <inheritdoc/>
    public void UpdateBankBalance(double delta)
    {
        _bankBalance = Math.Clamp(BankBalance + delta, double.MinValue, double.MaxValue);
    }
}
