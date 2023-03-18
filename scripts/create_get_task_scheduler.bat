@echo off
set "taskName=ShareDropGetCopyTask"
set "taskAction=powershell.exe -ExecutionPolicy Bypass -File C:\Users\Aviv9\source\repos\ShareDropApi\scripts\get_clipboard_text.ps1"
set "taskSchedule=/sc MONTHLY /st 05:00 /f"

schtasks /Create /tn "%taskName%" /tr "%taskAction%" %taskSchedule%