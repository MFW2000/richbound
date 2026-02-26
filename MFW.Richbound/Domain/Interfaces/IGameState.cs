using MFW.Richbound.Models;

namespace MFW.Richbound.Domain.Interfaces;

/// <summary>
/// Defines a contract for representing the state of the game.
/// </summary>
public interface IGameState
{
    /// <summary>
    /// The player character's first name.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// The player character's last name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// The player character's current health points that range from 0 to 100.
    /// </summary>
    int Health { get; }

    /// <summary>
    /// The player character's current hunger level that ranges from 0 to 100.
    /// </summary>
    int Hunger { get; }

    /// <summary>
    /// The player character's current thirst level that ranges from 0 to 100.
    /// </summary>
    int Thirst { get; }

    /// <summary>
    /// The player character's amount of money they have in their pockets.
    /// </summary>
    double PocketMoney { get; }

    /// <summary>
    /// The player character's bank balance, which can both be a positive and negative value.
    /// </summary>
    double BankBalance { get; }

    void Initialize(GameStateDto gameState);

    /// <summary>
    /// Updates the player character's health points up to a maximum of 100 and a minimum of 0.
    /// </summary>
    /// <param name="delta">The number of health points to add or subtract from the current value.</param>
    void UpdateHealth(int delta);

    /// <summary>
    /// Updates the player character's hunger level up to a maximum of 100 and a minimum of 0.
    /// </summary>
    /// <param name="delta">The number of hunger points to add or subtract from the current value.</param>
    void UpdateHunger(int delta);

    /// <summary>
    /// Updates the player character's thirst level up to a maximum of 100 and a minimum of 0.
    /// </summary>
    /// <param name="delta">The number of thirst points to add or subtract from the current value.</param>
    void UpdateThirst(int delta);

    /// <summary>
    /// Updates the player character's pocket money by the specified amount.
    /// </summary>
    /// <param name="delta">The amount of money to add or subtract from the current value.</param>
    void UpdatePocketMoney(double delta);

    /// <summary>
    /// Updates the player character's bank balance by the specified amount.
    /// </summary>
    /// <param name="delta">The amount of money to add or subtract from the current value.</param>
    void UpdateBankBalance(double delta);
}
