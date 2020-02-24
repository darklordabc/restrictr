@ECHO OFF
:B
SET MyProcess=vrserver.exe
ECHO "%MyProcess%"
TASKLIST | FINDSTR /I "%MyProcess%"
IF ERRORLEVEL 1 (ECHO "%MyProcess%" is not running) ELSE (GOTO :C)
timeout /t 5 /nobreak > NUL
GOTO :B

:C
SET MyProcess=restrictr.exe
ECHO "%MyProcess%"
TASKLIST | FINDSTR /I "%MyProcess%"
IF ERRORLEVEL 1 (GOTO :StartScripts) ELSE (ECHO "%MyProcess%" is running)
timeout /t 5 /nobreak > NUL
GOTO :B 

:D
SET MyProcess=restrictr.exe
ECHO "%MyProcess%"
TASKLIST | FINDSTR /I "%MyProcess%"
IF ERRORLEVEL 1 (timeout /t 5 /nobreak > NUL) ELSE (TASKKILL %MyProcess%)

GOTO :B 

:StartScripts 
CALL "restrictr.exe"
GOTO :B 