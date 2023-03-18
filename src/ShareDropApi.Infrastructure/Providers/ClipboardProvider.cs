using System.Diagnostics;
using ShareDropApi.Domain.Providers;

namespace ShareDropApi.Infrastructure.Providers;

public class ClipboardProvider : IClipboardProvider
{
    private readonly string _setClipboardTextFilePath;
    private readonly string _setClipboardTaskName;
    private readonly string _getClipboardTaskName;
    private readonly string _outputFilePath;

    public ClipboardProvider(string setClipboardTextFilePath, string setClipboardTaskName, string getClipboardTaskName, string outputFilePath)
    {
        _setClipboardTextFilePath = setClipboardTextFilePath;
        _setClipboardTaskName = setClipboardTaskName;
        _getClipboardTaskName = getClipboardTaskName;
        _outputFilePath = outputFilePath;
    }

    public async Task CreateCopyAsync(string text)
    {
        await CreateSetClipboardFile(text);
        StartClipboardTask(_setClipboardTaskName);
    }

    public async Task<string> GetCopyAsync()
    {
        StartClipboardTask(_getClipboardTaskName);
        await Task.Delay(1000);
        var text = await File.ReadAllTextAsync(_outputFilePath);

        return text;
    }

    private void StartClipboardTask(string taskName)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "schtasks.exe",
            Arguments = $"/run /tn \"{taskName}\"",
            WindowStyle = ProcessWindowStyle.Hidden
        };
        Process.Start(psi);
    }

    private async Task CreateSetClipboardFile(string text)
    {
        string[] newContent =
        {
            "Add-Type -AssemblyName PresentationCore",
            $"[Windows.Clipboard]::SetText('{text}')"
        };

        await File.WriteAllLinesAsync(_setClipboardTextFilePath, newContent);
    }
}