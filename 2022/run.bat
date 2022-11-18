@echo off
SETLOCAL EnableDelayedExpansion

REM ***
REM Runs aoc2022.ConsoleApp.
REM ***

dotnet run -p aoc2022.ConsoleApp -- %*
