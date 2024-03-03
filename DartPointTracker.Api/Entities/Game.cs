namespace DartPointTracker.Api;

public class Game
{
    public string GameId { get; set; }
    public int GamePoints { get; set; }
    public List<Player> Players { get; set; }

    public void CalculateELOs(List<Player> players, List<PlayerDto> places)
    {
        int n = players.Count;
        float K = 32 / (float)(n - 1);

        for (int i = 0; i < n; i++)
        {
            int curPlace = places.First(p => p.Id == players[i].Id).Place;
            int curELO = players[i].EloRankingScore;

            var eloChange = 0;
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    int opponentPlace = places.First(p => p.Id == players[j].Id).Place;
                    int opponentELO = players[j].EloRankingScore;

                    //work out S
                    float S;
                    if (curPlace < opponentPlace)
                        S = 1.0F;
                    else if (curPlace == opponentPlace)
                        S = 0.5F;
                    else
                        S = 0.0F;

                    //work out EA
                    float EA = 1 / (1.0f + (float)Math.Pow(10.0f, (opponentELO - curELO) / 400.0f));

                    //calculate ELO change vs this one opponent, add it to our change bucket
                    //I currently round at this point, this keeps rounding changes symetrical between EA and EB, but changes K more than it should
                    eloChange += (int)Math.Round(K * (S - EA));
                }
            }

            //add accumulated change to initial ELO for final ELO
            players[i].EloRankingScore = players[i].EloRankingScore + eloChange;
        }
    }
}