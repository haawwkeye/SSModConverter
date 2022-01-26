@echo off
title Finishing update...
move /-Y .\Update\* .\
RD /S /Q .\Update
Start ModMapConverter.exe
del /F /Q .\finishUpdate.bat