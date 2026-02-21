using MFW.Richbound.Models;

namespace MFW.Richbound.Infrastructure.Interfaces;

public interface ISaveFileManager
{
    Task<bool> SaveGameAsync(GameStateDto gameStateDto);

    Task<GameStateDto?> LoadGameAsync();

    bool HasSaveFile();
}
