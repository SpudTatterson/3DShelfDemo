@echo off

REM Change directory to the folder where the Python script is located
cd /d "%~dp0"

REM Get the IPv4 address of the computer (corrected to properly capture the full IP)
for /f "tokens=14 delims= " %%i in ('ipconfig ^| findstr /R /C:"IPv4 Address"') do set IPAddress=%%i

REM Remove "IPv4 Address. . . . . . . . . . . :" prefix if needed
set IPAddress=%IPAddress:~0%

REM Run the Python HTTP server on port 8000
start python -m http.server 8000

REM Wait for a few seconds to let the server start
timeout /t 3 /nobreak > nul

REM Display the IP address information
echo The server is running at http://%IPAddress%:8000
echo To connect from your phone or another device on your network, use the above IP address.

REM Open the default web browser to access the local server
start "" "http://%IPAddress%:8000"

pause
