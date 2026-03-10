using System.Text.Json;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure;

// TODO: Finished, write tests next.

/// <summary>
/// Implements <see cref="ISaveFileManager"/> for managing save files.
/// </summary>
public class SaveFileManager(IConsoleLogger consoleLogger) : ISaveFileManager
{
    private const string SaveFilePath = "save.json";

    /// <inheritdoc/>
    public bool SaveGame(GameStateDto gameStateDto)
    {
        try
        {
            var json = JsonSerializer.Serialize(gameStateDto);

            File.WriteAllText(SaveFilePath, json);

            return true;
        }
        catch (Exception exception)
        {
            consoleLogger.LogCritical(exception.Message);

            return false;
        }
    }

    /// <inheritdoc/>
    public GameStateDto? LoadGame()
    {
        try
        {
            var json = File.ReadAllText(SaveFilePath);

            return JsonSerializer.Deserialize<GameStateDto>(json);
        }
        catch (Exception exception)
        {
            consoleLogger.LogCritical(exception.Message);

            return null;
        }
    }

    /// <inheritdoc/>
    public bool HasSaveFile()
    {
        return File.Exists(SaveFilePath);
    }
}
