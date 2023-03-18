namespace ShareDropApi.Domain.Providers;

public interface IClipboardProvider
{
    Task CreateCopyAsync(string text);
    Task<string> GetCopyAsync();
}