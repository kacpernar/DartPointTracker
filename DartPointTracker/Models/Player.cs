namespace DartPointTracker.Models;

public class Player(string name)
{
    public string Id { get; set; }
    public string Name { get; set; } = name;
    public int Score { get; set; }
    public List<Throw> DartThrows { get; set; } = [];
    public int CurrentThrowNumber => DartThrows.Count;

    public int?[] CurrentRoundThrows = [null, null, null];
    public int ThrowNumberInGameAtWin { get; set; }
    public bool Won { get; set; }
    private bool LastRoundOverThrow { get; set; }
    public int EloRankingScore { get; set; }
    public int Rank { get; set; }


    public int CurrentRoundThrowNumber => CurrentThrowNumber % 3;

    public bool Selected { get; set; }
    public void StartNewRound()
    {
        CurrentRoundThrows = [null, null, null];
    }
    public (bool validThrow, int currentRoundThrowNumber) AddDartThrow(int points)
    {
        CurrentRoundThrows[CurrentRoundThrowNumber] = points;
        DartThrows.Add(new Throw(CurrentThrowNumber, points));
        Score -= points;
        switch (Score)
        {
            case < 0:
                Score += CurrentRoundThrows.Where(x => x.HasValue).Sum(x => x.Value);

                LastRoundOverThrow = true;
                return (false, CurrentRoundThrowNumber);
            case 0:
                Won = true;
                break;
        }

        return (true, CurrentRoundThrowNumber);
    }
    public void RemoveLastDartThrow()
    {
        if (CurrentThrowNumber == 0)
            return;

        var lastThrow = DartThrows.Last();
        DartThrows.Remove(lastThrow);
        CurrentRoundThrows[CurrentRoundThrowNumber] = 0;
        if (LastRoundOverThrow)
        {
            Score -= CurrentRoundThrows.Where(x => x.HasValue).Sum(x => x.Value);
            LastRoundOverThrow = false;
        }
        else
        {
            Score += lastThrow.Score;
        }

    }

    public void ReloadPreviousRound()
    {
        if(CurrentThrowNumber < 3)
            return;
        CurrentRoundThrows[0] = DartThrows[^3].Score;
        CurrentRoundThrows[1] = DartThrows[^2].Score;
        CurrentRoundThrows[2] = DartThrows[^1].Score;
    }

}

