namespace ShareDropApi.Domain.Providers;

public interface INotificationProvider
{
    void CreateFileToastNotification(string filePath);
    void CreateToastNotification(string title, string text);
}