@echo off
rem Generates the "VersionInfo.generated.cs" file.

setlocal
set sed=tools\gnu\sed.exe
pushd %~dp0

call setversionenv.bat
%sed% "s/=version=/%downmarkerversion%/" VersionInfo.template | %sed% "s/=id=/%downmarkerid%/" > VersionInfo.generated.cs
echo done: Written VersionInfo.generated.cs
popd
