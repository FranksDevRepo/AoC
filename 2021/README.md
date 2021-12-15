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

- `dotnet run --project aoc2021.WebApp`
- Open `http://localhost:52016/`

To run all puzzle solutions in console:

- `dotnet run --project aoc2021.ConsoleApp --all`

To run the last solution in console:

- `dotnet run --project aoc2021.ConsoleApp --last`

To run a specific solution in console:

- `dotnet run --project aoc2021.ConsoleApp --day` **`[number of day]`**

To setup the environment for a new puzzle solution:

- Set your [adventofcode.com](https://adventofcode.com) session cookie for `aoc2021.ConsoleApp` as a user secret:
  - `dotnet user-secrets --project aoc2021.ConsoleApp set "SessionCookie"` **`"Your session cookie"`**
- Run setup to create source, test, input and description files for the given day:
  - `dotnet run --project aoc2021.ConsoleApp --setup` **`[number of day]`**

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
  - [Solution Megathreads](https://www.reddit.com/r/adventofcode/wiki/solution_megathreads) 💡
  - [mega-thread solutions scraper](https://aocweb.yulrizka.com/) 💡
- YouTube
  - [Brad Wilson, xUnit maintainer](https://www.youtube.com/user/dotnetguy64)
  - [Brad Wilson on Twitch](https://www.twitch.tv/BradWilson72)
- GitHub
  - [Awesome Advent of Code](https://github.com/Bogdanp/awesome-advent-of-code)
  - [David Nemeth Cs.](https://github.com/encse/adventofcode) - smart and functional style, immutability and LINQ
  - [Eduardo Cáceres](https://github.com/eduherminio/AoC2021)
  - [AoCHelper](https://github.com/eduherminio/AoCHelper)
  - [AdventOfCode.Template](https://github.com/eduherminio/AdventOfCode.Template)
  - [Johannes Norrbacka](https://github.com/norrbacka/aoc2021) - functional style
  - [Giannis Ntovas](https://github.com/ntovas/AdventOfCode) - tries to solve with LINQ
  - [Stuart Turner](https://github.com/viceroypenguin/adventofcode/tree/master/2021) - smart
  - [David Camp](https://github.com/Bpendragon/AdventOfCodeCSharp/)
- F\#
  - [Kimmo Parviainen-Jalanko](https://github.com/kimvais/AoC2021)
- Python
  - [Jonathan Paulson](https://github.com/jonathanpaulson/AdventOfCode) , [streaming on YouTube](https://www.youtube.com/channel/UCuWLIm0l4sDpEe28t41WITA)

## notes on 2021 puzzles

| day | notes |
|----:|:------|
| 1   | Sonar Sweep - part 2 needed a sliding window sum, implemented by range ```numbers[i..upperRangeWindow].Sum()``` |
| 2   | Dive! - parsing commands by **regex** expression , used IPosition Interface to simplify code |
| 3   | Binary Diagnostic - checking bits in a row, although easy , I had some trouble finding a solution, data structures, used `Func<long, long, bool>`-Delegate to avoid code duplication in the `CalculateRating()`-Method |
| 4   | Giant Squid - bingo boards, data structure, part 1 find winning bingo board, part 2 find last winning bingo board, 🔨 could need some refactoring |
| 5   | Hydrothermal Venture - part 2 was tricky because of diagonals, 📈 visualisation would be nice, 🐎 could need some optimization |
| 6   | Lanternfish - solved part 1 - surprisingly, part 2 execution time seems to be exponential, 🐎 💩 needs optimization, need to look at this clever [circular shift register](https://www.reddit.com/r/adventofcode/comments/r9z49j/2021_day_6_solutions/hnfhi24/) solution by [David Nemeth Cs.](https://github.com/encse/adventofcode/blob/master/2021/Day06/Solution.cs) |
| 7   | The Treachery of Whales - rather straight forward, easy |
| 8   | Seven Segment Search - at first reading, seemed very complicated, but the first part was not. But I was overfitting the regular expression, so it didn't match. Second part was tricky to get one's head round, I looked into reddit to get an idea for an algorithm. Somehow it reminded me of a puzzle in 2020, where I solved a puzzle by an elimination approach: Using the 4 digits with a unique number of segments, you can find the digits with 5 segments (2, 3, 5) and the digits with 6 segments (6, 9, 0). The order of decoding in `Decode5CharSegments()` is significant. 💩 Ugly code in `DecodeXCharSegments()` |
| 9   | Smoke Basin - 🚧 |
| 10  | Syntax Scoring - attempted to solve the puzzle by a backreferencing regular expression, but it turned out to be a lot easier, to solve the puzzle with a simple stack, part ii caused an numeric overflow of an int32 so that my result was too low. But my mistake was easy to spot because the were negative values in the list of scores. 🔨 could need some refactoring |
| 11  | Dumbo Octopus - similar to days 11 part 2 (2d), 17 of 2020 (3d)? 🚧 |
| 12  | Passage Pathing - the problem involves a graph, I thought about implementing a graph algorithm with nodes, edges, ... But then I looked into a solution. Found it at [mega-thread solutions scraper](https://aocweb.yulrizka.com/?year=2021&day=12&language=C%23): [encse day 12 c# solution](https://reddit.com/r/adventofcode/comments/rehj2r/2021_day_12_solutions/ho8dez5/). Much simpler approach using only a (adjacency) dictionary of adjacent nodes). It is readable and compact. *for concept of graphs see C# Data Structures and Algorithms by Marcin Jamro* |
| 13  | Transparent Origami - first I thought the puzzle might be similar to puzzle 20 of 2020, but it wasn't. Very nice puzzle, with a little clue *New positions of folded dots easily calculated with: foldPos - (dotPos - foldPos)*, the solution was less complex than I thought it to be. Had to convert the `Hashset<Coordinate>` to a String to see the capital letters. 🔨 Could need some refactoring. |
| 14  | Extended Polymerization - first part with 10 iterations works, but the second part with 40 iterations needs optimization 🐎 runtime grows exponentially like puzzle 6 "Lanternfish", found a cool idea how to speed up the algorithm by a Python solution, after I had optimized the code, I got negative values which I first could not recognize as numeric overflow errors, so I digged deeper into debugging and finally found my error: instead of int I needed UInt64, 💩 up to now code looks ugly and needs refactoring 🔨 and cleanup IDictionaryExtensions.RepeatKeys too slow, proud I got an efficient solution for the second part |
| 15  | Chiton - Shortest path algorithm same problem as [Project Euler #83](https://projecteuler.net/problem=83) : [Djikstra's algorithm](https://github.com/matiii/Dijkstra.NET), [NetworkX](https://github.com/JakaJenko/INA_Graph_library) : 💡 I used Djikstra's algorithm as published in github repo of book [C# Data Structures and Algorithms](https://github.com/PacktPublishing/C-Sharp-Data-Structures-and-Algorithms), in first part I oversaw that you need to use a directed graph because edges (a -> b, b -> a) have different weights, the second part needs some time to compute 🐎, but the result was correct at first try, did some refactoring, so that I'm satisfied with the solution |
| 16  | |
| 17  | |
| 18  | |
| 19  | |

