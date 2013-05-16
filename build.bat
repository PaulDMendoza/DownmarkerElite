@echo off

rem Windows build script for downmarker.
rem Run without arguments for the full build, including setup.
rem Run with /test to stop after the unit tests.
rem Run with /install to immediately install the setup.

rem The only prerequisite is to have the Microsoft.NET 3.5 framework
rem installed. This is present by default on Windows 7. The script will
rem tell you where to download it if necessary.

rem If successfull, the downmarker.exe executable will appear in the
rem bin folder.

set here=%~dp0
set msbuild="%windir%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
set candle="%here%tools\wix\candle.exe"
set light="%here%tools\wix\light.exe"

echo === Checking environment ===
if not exist %msbuild% goto missing_net40
echo OK

echo === Building source code ===
%msbuild% "%here%downmarker.vs2010.sln" || goto build_error

echo === Running unit tests ===
"%here%tools\nunit\nunit-console.exe" /nologo "%here%bin\downmarker.tests.dll" || goto nunit_error
if "%1"=="/test" exit /b 0

echo === Creating setup ===
call setversionenv.bat
%candle% /nologo "%here%setup\product.wxs" -o "%here%bin\product.wixobj" || goto setup_error
%light% /sice:ICE91 /sice:ICE61 /sice:ICE57 /nologo -ext WixNetFxExtension "%here%bin\product.wixobj" -o "%here%bin\downmarker.msi" || goto setup_error

if "%1"=="/install" goto :install

echo === SUCCESS ===
echo You can now run bin\downmarker.exe or install bin\downmarker.msi
exit /b 0

:install
echo === Installing setup ===
msiexec /i "%here%\bin\downmarker.msi" || goto install_error
echo === SUCCESS ==
echo A new build of downmarker has been installed.
exit /b 0

:install_error
echo ERROR: installing downmarker failed.
exit /b 1

:missing_net40
echo ERROR: Microsoft .NET Framework 4.0 is not installed. You can download it here:
echo http://www.microsoft.com/downloads/en/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992^&displaylang=en
exit /b 1

:nunit_error
echo ERROR: there was a unit test failure
exit /b 2

:build_error
echo ERROR: downmarker build was not successful
exit /b 2

:setup_error
echo ERROR: failed to create setup
exit /b 2
