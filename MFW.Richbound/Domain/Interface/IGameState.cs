namespace MFW.Richbound.Domain.Interface;

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

    int Health { get; }

    int Hunger { get; }

    int Thirst { get; }

    double PocketMoney { get; }

    double BankBalance { get; }

    void UpdateHealth(int delta);

    void UpdateHunger(int delta);

    void UpdateThirst(int delta);

    void UpdatePocketMoney(double delta);

    void UpdateBankBalance(double delta);
}
