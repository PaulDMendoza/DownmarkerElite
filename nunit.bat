@echo off
setlocal
set root=%~dp0
start %root%tools\nunit\nunit.exe %root%bin\downmarker.tests.dll
