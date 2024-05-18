@echo off

:loop
tasklist | find /i "spotdl.exe" > nul
if %errorlevel% equ 0 (
    taskkill /f /im spotdl.exe
    timeout /t 1 /nobreak > nul
    goto :loop
)

tasklist | find /i "ytdlp.exe" > nul
if %errorlevel% equ 0 (
    taskkill /f /im ytdlp.exe
    timeout /t 1 /nobreak > nul
    goto :loop
)

tasklist | find /i "ffmpeg.exe" > nul
if %errorlevel% equ 0 (
    taskkill /f /im ffmpeg.exe
    timeout /t 1 /nobreak > nul
    goto :loop
)

exit
