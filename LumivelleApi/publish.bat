@echo off
REM Publish Tulpar.AspCore.Templates to NuGet.org
REM Usage: publish.bat <api-key>

if "%1"=="" (
  echo Usage: %0 ^<nuget-api-key^>
  echo.
  echo Get your API key from: https://www.nuget.org/account/ApiKeys
  echo Create with scope 'Push new packages and package versions'
  exit /b 1
)

setlocal enabledelayedexpansion

set API_KEY=%1
set REPO_ROOT=%~dp0
set NUPKG_DIR=%REPO_ROOT%nupkg
set TEMPLATE_PACK=%REPO_ROOT%template-pack

echo.
echo 🔨 Building template package...
if not exist "%NUPKG_DIR%" mkdir "%NUPKG_DIR%"
dotnet pack "%TEMPLATE_PACK%\Tulpar.AspCore.Templates.csproj" -o "%NUPKG_DIR%" -c Release

REM Extract version from csproj
for /f "tokens=* delims=" %%a in ('findstr /R "<PackageVersion>" "%TEMPLATE_PACK%\Tulpar.AspCore.Templates.csproj"') do (
  set "line=%%a"
  set "VERSION=!line:*<PackageVersion>=!"
  set "VERSION=!VERSION:</PackageVersion>*=!"
)

set PACKAGE=%NUPKG_DIR%\Tulpar.AspCore.Templates.%VERSION%.nupkg

if not exist "%PACKAGE%" (
  echo ❌ Package not found: %PACKAGE%
  exit /b 1
)

echo.
echo 📦 Package: %PACKAGE%
echo ✨ Pushing to NuGet.org...

dotnet nuget push "%PACKAGE%" ^
  -k %API_KEY% ^
  -s https://api.nuget.org/v3/index.json ^
  --skip-duplicate

echo.
echo ✅ Published successfully!
echo.
echo Users can now install with:
echo   dotnet new install Tulpar.AspCore.Templates
echo   dotnet new tulpar-aspcore -n MyApi
