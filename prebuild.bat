@echo off
pushd %~dp0

rem Sets the downmarkerversion environment variable from the hg revision.
call generateversioninfo.bat

rem Copy Kevin Burke's markdown.css into downmarker solution
copy external\kevinburke-markdowncss\markdown.css downmarker.core\styles\kevinburke-markdown.css

popd
