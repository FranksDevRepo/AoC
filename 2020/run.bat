@echo off
SETLOCAL EnableDelayedExpansion

REM ***
REM Runs aoc2020.ConsoleApp.
REM ***

dotnet run -p aoc2020.ConsoleApp -- %*
