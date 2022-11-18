# Advent of Code 2022 C# Blazor solutions [![.NET Core build badge](https://github.com/sanraith/aoc2021/workflows/.NET%20Core/badge.svg)](https://github.com/sanraith/aoc2021/actions)

Solutions for [Advent of Code 2022](https://adventofcode.com/2022) in C# with a Blazor WebAssembly runner. This project uses .Net Core 6.0  
Solutions can be run in console or directly inside a modern web browser, thanks to Blazor WebAssembly.

Try it at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/)

## Project structure

| Folder                 | Description
| ---                    | ---
| `aoc2022.ConsoleApp`   | Console application to prepare and run the puzzle solutions.
| `aoc2022.Puzzles`      | Inputs and solutions for the Advent of Code puzzles.
| `aoc2022.Puzzles.Test` | Unit tests for the solutions.
| `aoc2022.WebApp`       | Blazor WebAssembly application to run the puzzle solutions within a WebAssembly-compatible browser.
| `docs`                 | The published version of `aoc2022.WebApp`. Available at: [https://sanraith.github.io/aoc2019/](https://sanraith.github.io/aoc2019/).

## Build and run

Make sure `.NET Core SDK 6.0.100` or later is installed.  
Clone and build the solution:

- `git clone https://github.com/sanraith/aoc2019`
- `cd aoc2022`
- `dotnet build`

To run the Blazor WebAssembly application:

- `dotnet run --project aoc2022.WebApp`
- Open `http://localhost:52016/`

To run all puzzle solutions in console:

- `dotnet run --project aoc2022.ConsoleApp --all`

To run the last solution in console:

- `dotnet run --project aoc2022.ConsoleApp --last`

To run a specific solution in console:

- `dotnet run --project aoc2022.ConsoleApp --day` **`[number of day]`**

To setup the environment for a new puzzle solution:

- Set your [adventofcode.com](https://adventofcode.com) session cookie for `aoc2022.ConsoleApp` as a user secret:
  - `dotnet user-secrets --project aoc2022.ConsoleApp set "SessionCookie"` **`"Your session cookie"`**
- Run setup to create source, test, input and description files for the given day:
  - `dotnet run --project aoc2022.ConsoleApp --setup` **`[number of day]`**

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
  - [Mark Heath AoC 2015 in C# using LINQ, and F#](https://youtube.com/playlist?list=PLzUdt2T2KyAfhCdO3RRDmEl67pSEWMwzY), [Mark Heath Blog](https://markheath.net)
  - [Lurf Jurf, Python Live Coding](https://www.youtube.com/watch?v=KMzPf4om2k4)
- GitHub
  - [Awesome Advent of Code](https://github.com/Bogdanp/awesome-advent-of-code)
  - [Advent of Code Statistics of first top 100](https://www.maurits.vdschee.nl/scatterplot/), [Maurits van der Schee, Java Source](https://github.com/mevdschee/aoc-stats), [TQ dev.com mainly GopherCon](https://tqdev.com)
  - [David Nemeth Cs.](https://github.com/encse/adventofcode) - smart and functional style, immutability and LINQ
  - [Eduardo Cáceres](https://github.com/eduherminio/AoC2021)
  - [AoCHelper](https://github.com/eduherminio/AoCHelper)
  - [AdventOfCode.Template](https://github.com/eduherminio/AdventOfCode.Template)
  - [Johannes Norrbacka](https://github.com/norrbacka/aoc2021) - functional style
  - [Giannis Ntovas](https://github.com/ntovas/AdventOfCode) - tries to solve with LINQ
  - [Stuart Turner](https://github.com/viceroypenguin/adventofcode/tree/master/2021) - smart
  - [David Camp](https://github.com/Bpendragon/AdventOfCodeCSharp/) - Linq
  - [Dana Larose](https://github.com/DanaL/AdventOfCode) - ywgdana has some nice explanations on reddit 
  - [AlFasGD template](https://github.com/AlFasGD/AdventOfCSharp), [AlFasGD solutions 2015-](https://github.com/AlFasGD/AdventOfCode)
- F\#
  - [Kimmo Parviainen-Jalanko](https://github.com/kimvais/AoC2021)
  - [Baine Wedlock](https://github.com/bainewedlock/aoc-2021-fsharp)
- Python
  - [Jonathan Paulson](https://github.com/jonathanpaulson/AdventOfCode) , [streaming on YouTube](https://www.youtube.com/channel/UCuWLIm0l4sDpEe28t41WITA)
  - [4HbQ on reddit](https://www.reddit.com/r/adventofcode/comments/rds32p/2021_day_11_solutions/ho3ebqa/?context=8&depth=9)
- SQL
  - [Feike Steenbergen](https://gitlab.com/feike/adventofcode/)
- [Project Euler](https://projecteuler.net/) series of challenging mathematical/computer programming problems

## notes on 2021 puzzles

| day | notes |
|----:|:------|
| 1   | Sonar Sweep - part 2 needed a sliding window sum, implemented by range ```numbers[i..upperRangeWindow].Sum()``` |
| 2   | Dive! - parsing commands by **regex** expression , used IPosition Interface to simplify code |
| 3   | Binary Diagnostic - checking bits in a row, although easy , I had some trouble finding a solution, data structures, used `Func<long, long, bool>`-Delegate to avoid code duplication in the `CalculateRating()`-Method |
| 4   | Giant Squid - bingo boards, data structure, part 1 find winning bingo board, part 2 find last winning bingo board, 🔨 could need some refactoring |
| 5   | Hydrothermal Venture - part 2 was tricky because of diagonals, 📈 visualisation would be nice, 🐎 could need some optimization [LINQ solution](https://www.reddit.com/r/adventofcode/comments/r9824c/2021_day_5_solutions/hncsi2x/)|
| 6   | Lanternfish - solved part 1 - surprisingly, part 2 execution time seems to be exponential, 🐎 💩 needs optimization, need to look at this clever [circular shift register](https://www.reddit.com/r/adventofcode/comments/r9z49j/2021_day_6_solutions/hnfhi24/) solution by [David Nemeth Cs.](https://github.com/encse/adventofcode/blob/master/2021/Day06/Solution.cs) |
| 7   | The Treachery of Whales - rather straight forward, easy [LINQ solution](https://www.reddit.com/r/adventofcode/comments/rar7ty/2021_day_7_solutions/hnk6z1a/) |
| 8   | Seven Segment Search - at first reading, seemed very complicated, but the first part was not. But I was overfitting the regular expression, so it didn't match. Second part was tricky to get one's head round, I looked into reddit to get an idea for an algorithm. Somehow it reminded me of a puzzle in 2020, where I solved a puzzle by an elimination approach: Using the 4 digits with a unique number of segments, you can find the digits with 5 segments (2, 3, 5) and the digits with 6 segments (6, 9, 0). The order of decoding in `Decode5CharSegments()` is significant. 💩 Ugly code in `DecodeXCharSegments()` |
| 9   | Smoke Basin - 🚧 |
| 10  | Syntax Scoring - attempted to solve the puzzle by a backreferencing regular expression, but it turned out to be a lot easier, to solve the puzzle with a simple stack, part ii caused an numeric overflow of an int32 so that my result was too low. But my mistake was easy to spot because the were negative values in the list of scores. 🔨 could need some refactoring |
| 11  | Dumbo Octopus - similar to days 11 part 2 (2d), 17 of 2020 (3d)? 🚧 [ywgdana on reddit](https://www.reddit.com/r/adventofcode/comments/rds32p/2021_day_11_solutions/ho4wpi2/), [saahilclaypool oop solution on reddit](https://www.reddit.com/r/adventofcode/comments/rds32p/2021_day_11_solutions/ho5isyq/), [Outrageous72 on reddit](https://www.reddit.com/r/adventofcode/comments/rds32p/2021_day_11_solutions/ho3ekgq/), [aardvark1231](https://topaz.github.io/paste/#XQAAAQDpCgAAAAAAAAA6nMlWi076alCx9N1TtsVNiXecUoGeYT6aP6mR8mlULJpnBWlkXihMFNaRPYev6fF2iCZ7U+Cuf/y+wLt7aE4vFuBojMos7kq6n9Q96/JB58b5MVnhvn2aOltJaWIk4vAKarpi6qjnSt3bXFuyX9tp/6bA1FhlXamnMdNSCPf5VwUq9TdQYeQhPSxPWAdI5RAiP2m8SuqfxXNkHX/BI9Q2rHDAoFcg5Xo+pV7xNrNLB7aLzkI+6W0nYM1qCJR6tABy73siDTiKciUHfsNdDJFfMAnp3Qr9pBBGezx3nq7qqS/AnsRbdQtSacYEAuPN3TFmwx6I0j4dlffOsww85XMkxWTWxC82N/1nr/dj7HFmH/sylWgs7g2CF18LGBbsbFojUDY/7nM6VoL1GJSLRldfksnP6yKOutDw2juJtSdEza7xmTQcgZhHCbTmf7JnFyroGiJ3fEnVphlcq5N2So2hLm4nEwwAH+2Cq2Ox1ELeYcVNGeX3OmH1LHOSa4wPoy41OzuwxG6c9pS+TjZzQQfl9qljZ8Koebk6ctzkF5kgetQZeWAkeoeDjTHb+s3sj8x/VeI5DSzRcOC0O9+suMfz4pNIdpXtzmLjREZGLpo6YZdf2DxaPvYhzswuKEIZ5QAPgbuKOKbtQdKvQLlvRzRriGDQYnuv7bEsreyPUHgqs97lWPiKH2Lxiu0iAheNnO4OuWkZp2IDYA3z5WeyHgTH//gVEKerwUnnkEvkWsY9LAkSR/aBwQ9SRresv1jkSCJ4aJ8Za0rmbtKMZZWPZJE9WnUMGVZMHfb5+GqbZKVojWDISaCQP126IP9LALe/m4zZ1rqiNd+ZMhUaGkqFNJ/hIVmSkP4T50I5eyEpwfulLkU5nG/PtkiurKDw86KLm8YKFfBGwA7BWRGBKOyIsQ6B9/4864E=) |
| 12  | Passage Pathing - the problem involves a graph, I thought about implementing a graph algorithm with nodes, edges, ... But then I looked into a solution. Found it at [mega-thread solutions scraper](https://aocweb.yulrizka.com/?year=2021&day=12&language=C%23): [encse day 12 c# solution](https://reddit.com/r/adventofcode/comments/rehj2r/2021_day_12_solutions/ho8dez5/). Much simpler approach using only a (adjacency) dictionary of adjacent nodes). It is readable and compact. *for concept of graphs see C# Data Structures and Algorithms by Marcin Jamro* |
| 13  | Transparent Origami - first I thought the puzzle might be similar to puzzle 20 of 2020, but it wasn't. Very nice puzzle, with a little clue *New positions of folded dots easily calculated with: foldPos - (dotPos - foldPos)*, the solution was less complex than I thought it to be. Had to convert the `Hashset<Coordinate>` to a String to see the capital letters. 🔨 Could need some refactoring. |
| 14  | Extended Polymerization - first part with 10 iterations works, but the second part with 40 iterations needs optimization 🐎 runtime grows exponentially like puzzle 6 "Lanternfish", found a cool idea how to speed up the algorithm by a Python solution, after I had optimized the code, I got negative values which I first could not recognize as numeric overflow errors, so I digged deeper into debugging and finally found my error: instead of int I needed UInt64, 💩 up to now code looks ugly and needs refactoring 🔨 and cleanup IDictionaryExtensions.RepeatKeys too slow, proud I got an efficient solution for the second part |
| 15  | Chiton - Shortest path algorithm same problem as [Project Euler #83](https://projecteuler.net/problem=83) : [Djikstra's algorithm](https://github.com/matiii/Dijkstra.NET), [NetworkX](https://github.com/JakaJenko/INA_Graph_library), [A* search algorithm](https://en.wikipedia.org/wiki/A*_search_algorithm) : 💡 I used Djikstra's algorithm as published in github repo of book [C# Data Structures and Algorithms](https://github.com/PacktPublishing/C-Sharp-Data-Structures-and-Algorithms), in first part I oversaw that you need to use a directed graph because edges (a -> b, b -> a) have different weights, the second part needs about **2:35** min to compute on my new computer 🐎, but the result was correct at first try, did some refactoring, so that I'm satisfied with the solution. 🔨 .Net 6 has a PriorityQueue collection, so I could possibly remove the NuGet package OptimizedPriorityQueue. A* is supposed to be faster. |
| 16  | Packet Decoder - Found David's solution very clean and readable, made some minor modifications, had to work on Analytics reports, [Perska](https://github.com/Perska/AoC2021/blob/master/AoC2021/Days/Day16.cs), [David Camp](https://github.com/Bpendragon/AdventOfCodeCSharp/blob/01ebdd/AdventOfCode/Solutions/Year2021/Day16-Solution.cs), could use [BitArray](https://docs.microsoft.com/en-us/dotnet/api/system.collections.bitarray?view=net-6.0) |
| 17  | Trick Shot - solved first part by calculator and gauss formula [Gaußsche Summenformel](https://de.wikipedia.org/wiki/Gau%C3%9Fsche_Summenformel): ![\Large x=\frac{-b\pm\sqrt{b^2-4ac}}{2a}](https://latex.codecogs.com/svg.latex?\Large&space;x=\frac{n(n-1)}{2}=\frac{n^2+n}{2})  |
| 18  | Snailfish - |
| 19  | Beacon Scanner - |
| 20  | Trench Map - |
| 21  | Dirac Dice - |
| 22  | Reactor Reboot - tried to use brute force, works for part 1 if you did not miss this extra condition: *"The initialization procedure only uses cubes that have x, y, and z positions of at least -50 and at most 50. For now, ignore cubes outside this region"* 🐎|
| 23  | Amphipod - |
| 24  | Arithmetic Logic Unit - |
| 25  | Sea Cucumber - |

