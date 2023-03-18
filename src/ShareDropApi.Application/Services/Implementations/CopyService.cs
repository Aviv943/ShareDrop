using ShareDropApi.Application.Services.Interfaces;
using ShareDropApi.Domain.Providers;

namespace ShareDropApi.Application.Services.Implementations;

public class CopyService : ICopyService
{
    private readonly IClipboardProvider _clipboardProvider;
    private readonly INotificationProvider _notificationProvider;

    public CopyService(IClipboardProvider clipboardProvider, INotificationProvider notificationProvider)
    {
        _clipboardProvider = clipboardProvider;
        _notificationProvider = notificationProvider;
    }

    public async Task CreateAsync(string text)
    {
        await _clipboardProvider.CreateCopyAsync(text);
        _notificationProvider.CreateToastNotification("Copy To PC", text);
    }

    public async Task<string> GetAsync()
    {
        var text = await _clipboardProvider.GetCopyAsync();
        _notificationProvider.CreateToastNotification("Copy From PC", text);

        return text;
    }
}