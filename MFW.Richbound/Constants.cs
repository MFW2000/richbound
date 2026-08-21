namespace MFW.Richbound;

/// <summary>
/// Provides common values used throughout the application.
/// </summary>
public static class Constants
{
    // File paths
    public const string DefaultLogFile = "richbound.log";
    public const string DefaultSaveFile = "save.json";

    // General
    public const int DisplayDelayTimeMilliseconds = 2000;
    public const int MaxNameLength = 25;

    // Player stats
    public const int MaxCharacterStatValue = 100;
    public const int MinCharacterStatValue = 0;
    public const int HungerDrainPerHour = -3;

    // Hospital
    public const int HospitalStayDurationHours = 12;
    public const int HospitalBillPercentage = 10;
    public const int HospitalBillCap = 100000;

    // Sleep
    public const int SleepDurationHours = 8;
    public const int ComfortLevelSleepPoor = 60;
    public const int ComfortLevelSleepAverage = 80;
    public const int ComfortLevelSleepGood = 100;
}
