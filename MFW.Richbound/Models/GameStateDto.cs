namespace MFW.Richbound.Models;

public record GameStateDto(
    string FirstName,
    string LastName,
    int Health,
    int Hunger,
    int Thirst,
    double PocketMoney,
    double BankBalance);
