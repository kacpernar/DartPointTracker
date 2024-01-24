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

    public List<Player> Ranking { get; set; } = [];
    public List<Player> Players { get; set; }
    private Player CurrentPlayer { get; set; }

    public bool IsFinished { get; set; }

    private int _currentPlayerThrowNumber;
    private int _totalThrows;

    public void AddDartThrow(int points)
    {
        _totalThrows++;
        _currentPlayerThrowNumber++;
        var validThrow = CurrentPlayer.AddDartThrow(points);
        if (!validThrow)
        {
            NextPlayer();
        }
        else if (CurrentPlayer.Won)
        {
            Ranking.Add(CurrentPlayer);
            if (Ranking.Count == Players.Count - 1)
            {
                IsFinished = true;
                return;
            }
            NextPlayer();
        }
        else if (_currentPlayerThrowNumber == 3)
        {
            NextPlayer();
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
        if (_currentPlayerThrowNumber == 0)
        {
            PreviousPlayer();
        }
        else
        {
            _currentPlayerThrowNumber--;
        }

        if (CurrentPlayer.Won)
        {
            CurrentPlayer.Won = false;
            Ranking.Remove(CurrentPlayer);
        }
        CurrentPlayer.RemoveLastDartThrow();
    }

    private void PreviousPlayer()
    {
        CurrentPlayer.ReloadPreviousRound();
        var index = Players.IndexOf(CurrentPlayer);
        CurrentPlayer = index == 0 ? Players[^1] : Players[index - 1];
        _currentPlayerThrowNumber = 2;

    }
    private void NextPlayer()
    {
        _currentPlayerThrowNumber = 0;
        do
        {
            var index = Players.IndexOf(CurrentPlayer);
            CurrentPlayer = index == Players.Count - 1 ? Players[0] : Players[index + 1];
        }
        while (CurrentPlayer.Won);
        CurrentPlayer.StartNewRound();
    }

}