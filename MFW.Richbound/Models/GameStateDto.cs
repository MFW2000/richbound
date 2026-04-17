using MFW.Richbound.Enumerations;

namespace MFW.Richbound.Models;

/// <summary>
/// Represents the state of the game at a given point in time.
/// </summary>
public record GameStateDto(
    Gender Gender,
    string FirstName,
    string LastName,
    int Health,
    int Energy,
    int Hunger,
    double PocketMoney,
    double BankBalance,
    int Day,
    int Time,
    PromptType LastLocation,
    bool HasUsedHomelessShelter);
