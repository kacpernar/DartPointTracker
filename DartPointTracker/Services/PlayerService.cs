using System.Net.Http.Json;
using System.Text.Json;
using DartPointTracker.Dtos;
using DartPointTracker.Models;
using Microsoft.JSInterop;

namespace DartPointTracker;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _httpClient;

    private readonly IJSRuntime _jsRuntime;


    public List<Player> Players { get; set; } = new();

    public PlayerService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> CreatePlayer(string? name)
    {
        try
        {
            if (name == null)
            {
                return "Name cannot be empty";
            }
            var response = await _httpClient.PostAsync("/Player?playerName=" + name, null);
            if (response.IsSuccessStatusCode)
            {
                var player = await response.Content.ReadFromJsonAsync<Player>();
                if (player != null)
                {
                    Players.Add(player);
                    await CachePlayers(Players);
                    return null;
                }
            }
            return "Player with this name already exists";
        }
        catch (Exception)
        {
            return "Check your internet connection and try again.";
        }

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

    public async Task SendGame(Game game)
    {
        try
        {
            var playerList = game.Ranking.
                Select(player => new PlayerDto(Id: player.Id, Place: game.Ranking.IndexOf(player) + 1)).ToList();
            var responseMessage = await _httpClient.PostAsJsonAsync("/Game", playerList);
            if (responseMessage.IsSuccessStatusCode)
            {
                game.GameSended = true;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Failed to send game to API: {ex.Message}");
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
