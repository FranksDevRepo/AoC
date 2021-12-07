# Advent of Code 2021 C# Blazor solutions [![.NET Core build badge](https://github.com/sanraith/aoc2021/workflows/.NET%20Core/badge.svg)](https://github.com/sanraith/aoc2021/actions)

Solutions for [Advent of Code 2021](https://adventofcode.com/2021) in C# with a Blazor WebAssembly runner. This project uses .Net Core 6.0  
Solutions can be run in console or directly inside a modern web browser, thanks to Blazor WebAssembly.

Try it at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/)

## Project structure

| Folder                 | Description
| ---                    | ---
| `aoc2021.ConsoleApp`   | Console application to prepare and run the puzzle solutions.
| `aoc2021.Puzzles`      | Inputs and solutions for the Advent of Code puzzles.
| `aoc2021.Puzzles.Test` | Unit tests for the solutions.
| `aoc2021.WebApp`       | Blazor WebAssembly application to run the puzzle solutions within a WebAssembly-compatible browser.
| `docs`                 | The published version of `aoc2021.WebApp`. Available at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/).

## Build and run

Make sure `.NET Core SDK 6.0.100` or later is installed.  
Clone and build the solution:

- `git clone https://github.com/sanraith/aoc2019`
- `cd aoc2021`
- `dotnet build`

To run the Blazor WebAssembly application:

- `dotnet run -p aoc2021.WebApp`
- Open `http://localhost:52016/`

To run all puzzle solutions in console:

- `dotnet run -p aoc2021.ConsoleApp --all`

To run the last solution in console:

- `dotnet run -p aoc2021.ConsoleApp --last`

To run a specific solution in console:

- `dotnet run -p aoc2021.ConsoleApp --day` **`[number of day]`**

To setup the environment for a new puzzle solution:

- Set your [adventofcode.com](https://adventofcode.com) session cookie for `aoc2021.ConsoleApp` as a user secret:
  - `dotnet user-secrets -p aoc2021.ConsoleApp set "SessionCookie"` **`"Your session cookie"`**
- Run setup to create source, test, input and description files for the given day:
  - `dotnet run -p aoc2021.ConsoleApp --setup` **`[number of day]`**

## Links

- Parsing 
  - [RegExtract](https://github.com/sblom/RegExtract) Clean & simple idiomatic C# RegEx-based line parser that emits strongly typed results.
  - [RegExr](https://regexr.com/) Online-Tool for testing regular expressions
- Combinatorics
  - [Combinatorics](https://github.com/eoincampbell/combinatorics)
- Linq
  - [MoreLINQ](https://morelinq.github.io/)
  - [MoreLINQ Examples](https://github.com/morelinq/examples)
- Reddit
  - [Solution Megathreads](https://www.reddit.com/r/adventofcode/wiki/solution_megathreads)
- YouTube
  - [Brad Wilson, xUnit maintainer](https://www.youtube.com/user/dotnetguy64)
  - [Brad Wilson on Twitch](https://www.twitch.tv/BradWilson72)

## notes on 2021 puzzles

| day | notes |
|----:|:------|
| 1   | Sonar Sweep - part 2 needed a sliding window sum, implemented by range ```numbers[i..upperRangeWindow].Sum()``` |
| 2   | Dive! - parsing commands by **regex** expression , used IPosition Interface to simplify code |
| 3   | Binary Diagnostic - checking bits in a row, although easy , I had some trouble finding a solution, data structures, 🚧 part 2 to do |
| 4   | Giant Squid - bingo boards, data structure, part 1 find winning bingo board, part 2 find last winning bingo board, 🔨 could need some refactoring |
| 5   | Hydrothermal Venture - part 2 was tricky because of diagonals, 📈 visualisation would be nice|
| 6   | Lanternfish - solved part 1 - surprisingly, part 2 execution time seems to be exponential, 🐎 💩 needs optimization |
| 7   | The Treachery of Whales - rather straight forward, easy |
| 8   | |
| 9   | |
| 10  | |
| 11  | |
| 12  | |

