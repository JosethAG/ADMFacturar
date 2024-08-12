using Newtonsoft.Json;
using ADM.Models;
using System.Net.Http.Headers;

namespace ADM.Service
{
    public class MonsterApiService : IMonsterApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IjdiNmU4ZmMxOTJhOWMwMGQ3ZjFjMjdhODQ5ZWEwMDdhIiwiY3JlYXRlZF9hdCI6IjIwMjQtMDctMjdUMjI6NTk6MDIuOTM3MDkxIn0.Pb0_E2ccu8gzXsf9TW-C_X-_P0GHqwXp9UJ8-PbC2zg";
        private readonly string _apiUrl = "https://api.monsterapi.ai/v1/generate/txt2img";

        public MonsterApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GenerateImageAsync(string prompt, int steps = 50, int width = 512, int height = 512)
        {
            var requestBody = new
            {
                prompt = prompt,
                steps = steps,
                width = width,
                height = height
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(jsonResponse);
            return json.status_url;
        }

        public async Task<string> GetImageStatusAsync(string statusUrl)
        {
            var statusResponse = await _httpClient.GetAsync(statusUrl);
            statusResponse.EnsureSuccessStatusCode();

            var statusJsonResponse = await statusResponse.Content.ReadAsStringAsync();
            dynamic statusJson = JsonConvert.DeserializeObject(statusJsonResponse);
            return statusJson.result.output[0];
        }
    }
}
