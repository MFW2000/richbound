namespace MFW.Richbound.Models;

/// <summary>
/// Represents the state of the game at a given point in time.
/// </summary>
public record GameStateDto(
    string FirstName,
    string LastName,
    int Health,
    int Hunger,
    int Thirst,
    double PocketMoney,
    double BankBalance);
