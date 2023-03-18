namespace ShareDropApi.Application.Services.Interfaces
{
    public interface ICopyService
    {
        Task CreateAsync(string text);
        Task<string> GetAsync();
    }
}
