namespace DartPointTracker.Models;

public class Game
{
    public Game(List<Player> players, int gamePoints)
    {
        GamePoints = gamePoints;
        foreach (var player in players)
        {
            player.Score = gamePoints;
        }

        Players = players;
        CurrentPlayer = players[0];
    }

    private int GamePoints { get; set; }
    public List<Player> Players { get; set; }
    public Player CurrentPlayer { get; set; }

    private int _currentPlayerThrowNubmer;
    private int _totalThrows;

    public void AddDartThrow(int points)
    {
        _totalThrows++;
        _currentPlayerThrowNubmer++;
        CurrentPlayer.AddDartThrow(points);
        if (_currentPlayerThrowNubmer == 3)
        {
            _currentPlayerThrowNubmer = 0;
            NextPlayer();
            CurrentPlayer.StartNewRound();
        }

    }
    public void RemoveLastDartThrow()
    {
        if (_totalThrows > 0)
        {
            _totalThrows--;
        }
        else
        {
            return;
        }
        if (_currentPlayerThrowNubmer == 0)
        {
            _currentPlayerThrowNubmer = 2;
            CurrentPlayer.ReloadPreviousRound();
            PreviousPlayer();
        }
        else
        {
            _currentPlayerThrowNubmer--;
        }
        CurrentPlayer.RemoveLastDartThrow();
    }

    private void PreviousPlayer()
    {
        var index = Players.IndexOf(CurrentPlayer);
        CurrentPlayer = index == 0 ? Players[^1] : Players[index - 1];
    }
    private void NextPlayer()
    {
        var index = Players.IndexOf(CurrentPlayer);
        CurrentPlayer = index == Players.Count - 1 ? Players[0] : Players[index + 1];
    }

}