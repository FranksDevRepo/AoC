@echo off
SETLOCAL EnableDelayedExpansion

REM ***
REM Runs aoc2021.ConsoleApp.
REM ***

dotnet run -p aoc2021.ConsoleApp -- %*
