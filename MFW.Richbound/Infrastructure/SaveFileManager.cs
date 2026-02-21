using System.Text.Json;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure;

public class SaveFileManager(IConsoleLogger consoleLogger) : ISaveFileManager
{
    private const string SaveFilePath = "save.json";

    public async Task<bool> SaveGameAsync(GameStateDto gameStateDto)
    {
        try
        {
            var json = JsonSerializer.Serialize(gameStateDto);

            await File.WriteAllTextAsync(SaveFilePath, json);

            return true;
        }
        catch (Exception exception)
        {
            consoleLogger.LogError(exception.Message);

            return false;
        }
    }

    public async Task<GameStateDto?> LoadGameAsync()
    {
        try
        {
            var json = await File.ReadAllTextAsync(SaveFilePath);

            return JsonSerializer.Deserialize<GameStateDto>(json);
        }
        catch (Exception exception)
        {
            consoleLogger.LogError(exception.Message);

            return null;
        }
    }

    public bool HasSaveFile()
    {
        return File.Exists(SaveFilePath);
    }
}
