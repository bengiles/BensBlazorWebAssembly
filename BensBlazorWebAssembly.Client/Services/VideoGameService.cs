using BensBlazorWebAssembly.Common.Entities;
using System.Net.Http.Json;

namespace BensBlazorWebAssembly.Client.Services
{
    public interface IVideoGameService
    {
        public Task<IEnumerable<VideoGame>> GetVideoGamesAsync();
        public Task<IEnumerable<VideoGame>> GetVideoGamesAsync_V2();
        public Task<VideoGame> GetVideoGameAsync(int id);
        public Task<VideoGame> AddVideoGameAsync(VideoGame videoGame);
        public Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGame);
        public Task<VideoGame> DeleteVideoGameAsync(int id);

    }
    public class VideoGameService : IVideoGameService
    {
        private readonly HttpClient _httpClient;
        public VideoGameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<VideoGame>>("api/v1/VideoGames");
        }
        public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync_V2()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<VideoGame>>("api/v2/VideoGames");
        }
        public async Task<VideoGame> GetVideoGameAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VideoGame>($"api/v1/VideoGames/{id}");
        }
        public async Task<VideoGame> AddVideoGameAsync(VideoGame videoGame)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/VideoGames", videoGame);
            return await response.Content.ReadFromJsonAsync<VideoGame>();
        }
        public async Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGame)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/v1/VideoGames/{videoGame.Id}", videoGame);
            return await response.Content.ReadFromJsonAsync<VideoGame>();
        }
        public async Task<VideoGame> DeleteVideoGameAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/v1/VideoGames/{id}");
            return await response.Content.ReadFromJsonAsync<VideoGame>();
        }
    }
}
