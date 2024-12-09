..\pritater\Python312\python.exe main.py -%1


:loop
timeout /t 0.1 >nul
tasklist | findstr /i python.exe >nul
if %errorlevel% equ 0 (
    goto loop
) else (
    exit
)
