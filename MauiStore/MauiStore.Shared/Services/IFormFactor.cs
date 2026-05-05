using MauiStore.Data;
using System.Net.Http.Json;

namespace MauiStore.Shared.Services
{
    public interface IFormFactor
    {
        public string GetFormFactor();
        public string GetPlatform();
    }

    public class PostService
    {
        private readonly HttpClient _http;

        public PostService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts = await _http.GetFromJsonAsync<List<Post>>("posts");
            return posts ?? new List<Post>();
        }
    }
}
