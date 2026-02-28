using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure.Interfaces;

/// <summary>
/// Defines a contract for managing save files.
/// </summary>
public interface ISaveFileManager
{
    /// <summary>
    /// Saves the specified game state to a JSON file.
    /// </summary>
    /// <param name="gameStateDto">The game state to save.</param>
    /// <returns>True if the save operation was successful, otherwise false.</returns>
    Task<bool> SaveGameAsync(GameStateDto gameStateDto);

    /// <summary>
    /// Loads the game state from a JSON file.
    /// </summary>
    /// <returns>The deserialized game state, or null if the load operation failed.</returns>
    Task<GameStateDto?> LoadGameAsync();

    /// <summary>
    /// Checks if a save file exists.
    /// </summary>
    /// <returns>True if a save file exists, otherwise false.</returns>
    bool HasSaveFile();
}
