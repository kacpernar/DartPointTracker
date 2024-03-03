using DartPointTracker.Models;

namespace DartPointTracker.Services;

public interface IGameService
{
    public Game? CurrentGame { get; set; }

    public Task InitializeGame(List<Player> players, int gamePoints);
}