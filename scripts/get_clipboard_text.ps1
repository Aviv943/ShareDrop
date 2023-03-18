Add-Type -AssemblyName System.Windows.Forms
$clipboardText = [System.Windows.Forms.Clipboard]::GetText().Split("`n")[-1]
$clipboardText | Out-File C:\Users\\Aviv9\source\repos\ShareDropApi\scripts\output.txt