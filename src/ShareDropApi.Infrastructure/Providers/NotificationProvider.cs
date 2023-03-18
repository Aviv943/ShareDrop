using System.Text.RegularExpressions;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using ShareDropApi.Domain.Providers;

namespace ShareDropApi.Infrastructure.Providers;

public class NotificationProvider : INotificationProvider
{
    public void CreateFileToastNotification(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var directoryPath = Path.GetDirectoryName(filePath);
        var logoUri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\logo.png"));

        ToastContentBuilder? contentBuilder = new ToastContentBuilder()
            .AddText("ShareDrop")
            .AddText(fileName)
            .AddAppLogoOverride(logoUri, ToastGenericAppLogoCrop.Circle)
            .AddButton("Open File", ToastActivationType.Protocol, filePath)
            .AddButton("Open File Location", ToastActivationType.Protocol, directoryPath)
            .SetToastDuration(ToastDuration.Long)
            .AddToastActivationInfo("protocol", ToastActivationType.Protocol);

        var photoRegex = new Regex(@"\.(jpg|jpeg|png|gif|bmp)$", RegexOptions.IgnoreCase);

        if (photoRegex.IsMatch(fileName))
        {
            contentBuilder.AddHeroImage(new Uri(filePath));
        }

        ToastContent? content = contentBuilder.GetToastContent();
        ToastNotification notification = new ToastNotification(content.GetXml());
        ToastNotificationManager.CreateToastNotifier(" ").Show(notification);
    }

    public void CreateToastNotification(string title, string text)
    {
        var logoUri = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\logo.png"));

        ToastContentBuilder? contentBuilder = new ToastContentBuilder()
            .AddText("ShareDrop")
            .AddText(title)
            .AddText(text)
            .AddAppLogoOverride(logoUri, ToastGenericAppLogoCrop.Circle)
            .SetToastDuration(ToastDuration.Short)
            .AddToastActivationInfo("protocol", ToastActivationType.Protocol);

        ToastContent? content = contentBuilder.GetToastContent();
        ToastNotification notification = new ToastNotification(content.GetXml());
        ToastNotificationManager.CreateToastNotifier(" ").Show(notification);
    }
}