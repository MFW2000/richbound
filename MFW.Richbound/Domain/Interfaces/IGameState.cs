using MFW.Richbound.Enumerations;
using MFW.Richbound.Models;

namespace MFW.Richbound.Domain.Interfaces;

/// <summary>
/// Defines a contract for representing the state of the game.
/// </summary>
public interface IGameState
{
    /// <summary>
    /// The player character's gender.
    /// </summary>
    Gender Gender { get;  }

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
    /// The player character's current energy level that ranges from 0 to 100.
    /// </summary>
    int Energy { get; }

    /// <summary>
    /// The player character's amount of money they have in their pockets.
    /// </summary>
    double PocketMoney { get; }

    /// <summary>
    /// The player character's bank balance, which can both be a positive and negative value.
    /// </summary>
    double BankBalance { get; }

    /// <summary>
    /// Day counter since starting the game.
    /// </summary>
    int Day { get; }

    /// <summary>
    /// The current time of the day in hours.
    /// </summary>
    int Time { get; }

    /// <summary>
    /// The last location the player character was in.
    /// </summary>
    PromptType LastLocation { get; }

    /// <summary>
    /// The formatted <see cref="Time"/> to display.
    /// </summary>
    string FormattedTime { get; }

    /// <summary>
    /// The player character's full name.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// The player character's title as either 'Mr.' or 'Ms.' based on their gender.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Initializes the game state with the provided game state data.
    /// </summary>
    /// <param name="gameStateDto">The game state data to initialize the game state with.</param>
    void Initialize(GameStateDto gameStateDto);

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
    /// Updates the player character's energy level up to a maximum of 100 and a minimum of 0.
    /// </summary>
    /// <param name="delta">The number of energy points to add or subtract from the current value.</param>
    void UpdateEnergy(int delta);

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

    /// <summary>
    /// Increments the day counter by one.
    /// </summary>
    void UpdateDay();

    /// <summary>
    /// Updates the time by adding the given hours to it. Remaining hours after 24 hours will be added after the time
    /// was reset to 0.
    /// </summary>
    /// <param name="hours">The number of hours to add.</param>
    /// <remarks>Negative hour values will be ignored.</remarks>
    void UpdateTime(int hours);

    /// <summary>
    /// Updates the player character's last location.
    /// </summary>
    /// <param name="newLocation">The new location the player character is in.</param>
    void UpdateLastLocation(LocationPromptType newLocation);
}
