# Advent of Code 2020 C# Blazor solutions [![.NET Core build badge](https://github.com/sanraith/aoc2020/workflows/.NET%20Core/badge.svg)](https://github.com/sanraith/aoc2020/actions)

Solutions for [Advent of Code 2020](https://adventofcode.com/2020) in C# with a Blazor WebAssembly runner. This project uses .Net Core 3.1.  
Solutions can be run in console or directly inside a modern web browser, thanks to Blazor WebAssembly.

Try it at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/)

## Project structure

| Folder                 | Description
| ---                    | ---
| `aoc2020.ConsoleApp`   | Console application to prepare and run the puzzle solutions.
| `aoc2020.Puzzles`      | Inputs and solutions for the Advent of Code puzzles.
| `aoc2020.Puzzles.Test` | Unit tests for the solutions.
| `aoc2020.WebApp`       | Blazor WebAssembly application to run the puzzle solutions within a WebAssembly-compatible browser.
| `docs`                 | The published version of `aoc2020.WebApp`. Available at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/).

## Build and run

Make sure `.NET Core SDK 3.1.100` or later is installed.  
Clone and build the solution:

- `git clone https://github.com/sanraith/aoc2019`
- `cd aoc2020`
- `dotnet build`

To run the Blazor WebAssembly application:

- `dotnet run -p aoc2020.WebApp`
- Open `http://localhost:52016/`

To run all puzzle solutions in console:

- `dotnet run -p aoc2020.ConsoleApp --all`

To run the last solution in console:

- `dotnet run -p aoc2020.ConsoleApp --last`

To run a specific solution in console:

- `dotnet run -p aoc2020.ConsoleApp --day` **`[number of day]`**

To setup the environment for a new puzzle solution:

- Set your [adventofcode.com](https://adventofcode.com) session cookie for `aoc2020.ConsoleApp` as a user secret:
  - `dotnet user-secrets -p aoc2020.ConsoleApp set "SessionCookie"` **`"Your session cookie"`**
- Run setup to create source, test, input and description files for the given day:
  - `dotnet run -p aoc2020.ConsoleApp --setup` **`[number of day]`**
