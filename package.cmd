@echo off
if "%1"=="" goto blank
if "%1"=="pack" goto pack

:push
rem Here is the publish section
if "%OTE_KEY%"=="" goto nokey
set nugetKey=%OTE_KEY%

if "%2"=="" goto blank
set packageName=.%2
if "%2"=="core" set packageName=

FOR /F "delims=|" %%I IN ('DIR "dist\OneTrueError.Client%packageName%*.nupkg" /B /O:D') DO SET packageName=%%I
src\.nuget\nuget push dist\%packageName% %nugetKey%
goto end

:pack
if "%2"=="" goto blank
set packageName=.%2
if "%2"=="core" set packageName=""

set msbuild.exe=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set msbuild.exe=%%D\MSBuild.exe
%msbuild.exe% src\OneTrueError.Client%packageName%\OneTrueError.client%packageName%.csproj /t:Build /p:Configuration=Release /nologo
if not exist "dist" mkdir dist
src\.nuget\nuget pack src\OneTrueError.Client%packageName%\OneTrueError.client%packageName%.csproj -OutputDirectory dist -Prop Configuration=Release


goto end

:blank
echo To pack package, write:
echo publish pack [clientType]
echo
echo  Where client type is "log4net" ;)
echo
echo
echo To push package run
echo publish nugetpackages\yourPackage.nukpg
goto end

:nokey
echo You must set an environment variable named "OTE_KEY" which contains the nuget key used to publish the package.


:end