namespace DartPointTracker.Models;

public class Game
{
    public Game(List<Player> players, int gamePoints)
    {
        GameId = Guid.NewGuid().ToString();
        GamePoints = gamePoints;
        foreach (var player in players)
        {
            player.Score = gamePoints;
        }

        Players = players;
        CurrentPlayer = players[0];
    }

    string GameId { get; set; }
    private int GamePoints { get; set; }

    public List<Player> Players { get; set; }

    public List<Player> Ranking { get; set; } = [];
    public Player CurrentPlayer { get; set; }

    public bool IsFinished { get; set; }

    private int _totalThrows;

    public void AddDartThrow(int points)
    {
        _totalThrows++;
        var (validThrow, currentRoundThrowNumber) = CurrentPlayer.AddDartThrow(points);

        if (CurrentPlayer.Won)
        {
            Ranking.Add(CurrentPlayer);
            CurrentPlayer.ThrowNumberInGameAtWin = _totalThrows;
            if (Players.Count(x => x.Won) == Players.Count - 1)
            {
                IsFinished = true;
                Ranking.Add(Players.First(x => !x.Won));
                return;
            }
            NextPlayer();
        }
        else if (!validThrow || currentRoundThrowNumber == 0)
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
        if (CurrentPlayer.CurrentRoundThrowNumber == 0)
        {
            CurrentPlayer.ReloadPreviousRound();
            PreviousPlayer();
        }

        while (CurrentPlayer.Won)
        {
            if (CurrentPlayer.ThrowNumberInGameAtWin == _totalThrows + 1)
            {
                CurrentPlayer.Won = false;
                break;
            }
            PreviousPlayer();
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
        do
        {
            var index = Players.IndexOf(CurrentPlayer);
            CurrentPlayer = index == Players.Count - 1 ? Players[0] : Players[index + 1];
        }
        while (CurrentPlayer.Won);
        CurrentPlayer.StartNewRound();
    }

}