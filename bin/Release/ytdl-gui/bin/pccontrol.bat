@echo off
:loop
tasklist /FI "IMAGENAME eq ytdl-gui.exe" 2>NUL | find /I /N "ytdl-gui.exe">NUL
if "%ERRORLEVEL%"=="0" (
    taskkill /F /IM "ytdl-gui.exe" /T
)
timeout /t 1
goto loop
