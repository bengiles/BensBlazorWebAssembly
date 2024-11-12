using BensBlazorWebAssembly.Common.Entities;
using System.Net.Http.Json;
using BensBlazorWebAssembly.Client.Api;
using RestSharp;
using BensBlazorWebAssembly.Client.Pages;

namespace BensBlazorWebAssembly.Client.Services
{
    public interface IVideoGameService
    {
        public Task<IEnumerable<VideoGame>> GetVideoGamesAsync();
        public Task<IEnumerable<VideoGame>> GetVideoGamesAsync_V2();
        public Task<VideoGame> GetVideoGameAsync(int id);
        public Task<VideoGame> AddVideoGameAsync(VideoGame videoGame);
        public Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGame);
        public Task<bool> DeleteVideoGameAsync(int id);

    }
    public class VideoGameService : IVideoGameService
    {
        private readonly HttpClient _httpClient;
        private IRestSharpHelper restSharpHelper { get; set; }
        public VideoGameService(HttpClient httpClient, IRestSharpHelper restSharpHelper)
        {
            _httpClient = httpClient;
            this.restSharpHelper = restSharpHelper;
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
            try
            {
                return await restSharpHelper.AsyncRequest<VideoGame>(Method.Post, "api/v1.0/VideoGames/new", videoGame);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return default;
            }
            
        }
        public async Task<VideoGame> UpdateVideoGameAsync(VideoGame videoGame)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/v1/VideoGames/{videoGame.Id}", videoGame);
            return await response.Content.ReadFromJsonAsync<VideoGame>();
        }
        public async Task<bool> DeleteVideoGameAsync(int id)
        {
            try
            {
                return await restSharpHelper.AsyncRequest(Method.Delete, $"api/v1/VideoGames/delete/{id}", null);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
    }
}
