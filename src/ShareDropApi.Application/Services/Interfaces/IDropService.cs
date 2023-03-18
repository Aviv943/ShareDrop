namespace ShareDropApi.Application.Services.Interfaces;

public interface IDropService
{
    Task UploadAsync(Stream stream, string fileName);
}