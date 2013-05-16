@echo off
rem Sets the downmarkerversion environment variable from the hg revision.

pushd %~dp0

hg log -l 1 --template "{latesttag}.{latesttagdistance}.0" > output.tmp
set /p downmarkerversion= < output.tmp

hg id > output.tmp
set /p downmarkerid= < output.tmp

del output.tmp

popd
