using System.Net.Http.Json;
using System.Text.Json;
using DartPointTracker.Models;
using Microsoft.JSInterop;

namespace DartPointTracker;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _httpClient;

    private readonly IJSRuntime _jsRuntime;


    public List<Player> Players { get; set; } = new List<Player>();

    public PlayerService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> CreatePlayer(string name)
    {
        var response = await _httpClient.PostAsync("/Player?playerName=" + name, null);
        if (response.IsSuccessStatusCode)
        {
            var player = await response.Content.ReadFromJsonAsync<Player>();
            if (player != null)
            {
                Players.Add(player);
                await CachePlayers(Players);
                return true;
            }
        }
        return false;
    }

    public async Task GetPlayers()
    {
        var players = await FetchPlayersFromApi();
        if (players != null && players.Count != 0)
        {
            Players.AddRange(players);
            await CachePlayers(Players);
        }
        else
        {
            players = await GetCachedPlayers();
            if (players != null && players.Count != 0)
            {
                Players.AddRange(players);
            }
        }
    }
    public static List<Player> GetRanking(List<Player> players)
    {
        // Order players by descending EloRankingScore
        var rankedPlayers = players.OrderByDescending(p => p.EloRankingScore).ToList();

        // Assign ranking based on the order
        for (int i = 0; i < rankedPlayers.Count; i++)
        {
            rankedPlayers[i].Rank = i + 1;
        }

        return rankedPlayers;
    }

    private async Task<List<Player>?> FetchPlayersFromApi()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Player>>("/Players");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to fetch players from API: {ex.Message}");
            return null;
        }
    }

    private async Task CachePlayers(List<Player>? players)
    {
        // Store the players in browser local storage
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "players", JsonSerializer.Serialize(players));
    }

    private async Task<List<Player>?> GetCachedPlayers()
    {
        // Retrieve players from browser local storage
        var playersJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "players");
        return !string.IsNullOrWhiteSpace(playersJson) ? JsonSerializer.Deserialize<List<Player>>(playersJson) :
            // No cached players found
            new List<Player>();
    }
}
