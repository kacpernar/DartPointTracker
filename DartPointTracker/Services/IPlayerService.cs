using DartPointTracker.Models;

namespace DartPointTracker;

public interface IPlayerService
{
    Task GetPlayers();
    Task<string?> CreatePlayer(string? name);
    List<Player> Players { get; set; }
    Task SendGame(Game game);
}