using ShareDropApi.Application.Services.Interfaces;
using ShareDropApi.Domain;
using ShareDropApi.Domain.Providers;
using System.IO;

namespace ShareDropApi.Application.Services.Implementations;

public class DropService : IDropService
{
    private readonly INotificationProvider _notificationProvider;
    private readonly string _destinationFolder;

    public DropService(INotificationProvider notificationProvider, string destinationFolder)
    {
        _notificationProvider = notificationProvider;
        _destinationFolder = destinationFolder;
    }

    public async Task UploadAsync(Stream stream, string fileName)
    {
        var destinationPath = Path.Combine(_destinationFolder, fileName);

        await using (var fileStream = new FileStream(destinationPath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
        }

        _notificationProvider.CreateFileToastNotification(destinationPath);


        //var originalFilePath = Path.Combine(_folderPath, fileName);
        //var tempFilePath = Path.Combine(_tempFolderPath, fileName);
        //File.Copy(originalFilePath, tempFilePath);
        //_notificationProvider.CreateFileToastNotification(fileName);
        //await Task.Delay(1000);
        //File.Delete(tempFilePath);
    }
}