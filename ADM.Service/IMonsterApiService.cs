namespace ADM.Service
{
    public interface IMonsterApiService
    {
        Task<string> GenerateImageAsync(string prompt, int steps = 50, int width = 512, int height = 512);
        Task<string> GetImageStatusAsync(string statusUrl);
    }
}
