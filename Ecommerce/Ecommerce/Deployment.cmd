@IF "%1" == "" GOTO USAGE

@powershell -NonInteractive -ExecutionPolicy bypass -Command "& {$identity = [System.Security.Principal.WindowsIdentity]::GetCurrent(); $principal = New-Object System.Security.Principal.WindowsPrincipal($identity); $administrators = [System.Security.Principal.WindowsBuiltInRole]::Administrator; if( $principal.IsInRole($administrators)){ exit(0); }else{ exit(1);} }"
@IF %ERRORLEVEL% NEQ 0 GOTO ELEVATE
@sqlcmd -S %1 -E -d master -i Setup_Dev.sql

SET sqlPackageExe="%ProgramFiles(x86)%\Microsoft SQL Server\130\DAC\bin\SqlPackage.exe"
IF EXIST "%ProgramFiles(x86)%\Microsoft SQL Server\120\DAC\bin\SqlPackage.exe" SET sqlPackageExe="%ProgramFiles(x86)%\Microsoft SQL Server\120\DAC\bin\SqlPackage.exe"

ECHO SqlPackage path: %sqlPackageExe% 
@%sqlPackageExe% /Action:publish /TargetConnectionString:"server=%1;Database=Ecommerce" /SourceFile:Ecommerce.dacpac 
@GOTO :EOF
:ELEVATE
@echo "Please run this script with elevated privileges."
@pause
GOTO EXIT

:USAGE
@ECHO Missing servername argument
@ECHO Usage: %0 servername

:EXIT
