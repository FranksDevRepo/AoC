﻿using CommandLine;
using CommandLine.Text;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using aoc2022.Puzzles.Core;

namespace aoc2022.ConsoleApp;

public sealed class Program
{
    private sealed class Options
    {
        [Option('a', "all", HelpText = "Run all available solutions.")]
        public bool RunAllDays { get; private init; }

        [Option('l', "last", HelpText = "Run the last available solution.")]
        public bool RunLastDay { get; init; }

        [Option('d', "day", HelpText = "[Number of day] Run the solution for the given day.")]
        public int? DayToRun { get; private init; }

        [Option('s', "setup", HelpText = "[Number of day] Download input and description for given day, and add it to aoc2022.Puzzles along with an empty solution .cs file.")]
        public int? DayToSetup { get; private init; }

        [Usage(ApplicationAlias = "aoc2022.ConsoleApp")]
        public static IEnumerable<Example> Examples => new[]
        {
            new Example("Run all available solutions", new Options { RunAllDays = true }),
            new Example("Run the last available solution", new Options { RunLastDay = true }),
            new Example("Run solution for day 12", new UnParserSettings{ PreferShortName = true }, new Options { DayToRun = 12 }),
            new Example("Add input and description for day 23 to aoc2022.Puzzles along with an empty test and solution .cs file", new Options { DayToSetup = 23 }),
        };
    }

    public static async Task Main(string[] args) => await new Program(args).Run().ConfigureAwait(false);

    public Program(string[] args)
    {
        Options options = null;
        if (!args.Any())
        {
            Console.WriteLine("Running last available solution. Use --help for available options.");
            Console.WriteLine();
            options = new Options { RunLastDay = true };
        }
        else
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o => options = o);
        }
        myConfig = Configuration.Load();
        myOptions = options;
        mySolutionHandler = new SolutionHandler();
    }

    public async Task Run()
    {
        if (myOptions == null) { return; }

        if (myOptions.DayToSetup.HasValue)
        {
            await SetupDay(myOptions.DayToSetup.Value).ConfigureAwait(false);
        }

        if (myOptions.RunAllDays)
        {
            await SolveAllDays().ConfigureAwait(false);
            return;
        }

        if (myOptions.RunLastDay)
        {
            await SolveLastDay().ConfigureAwait(false);
        }

        if (myOptions.DayToRun.HasValue)
        {
            await SolveDay(myOptions.DayToRun.Value).ConfigureAwait(false);
        }
    }

    private async Task SolveAllDays()
    {
        var count = 0;
        foreach (var day in mySolutionHandler.Solutions.Keys.OrderBy(x => x))
        {
            await SolveDay(day).ConfigureAwait(false);
            if (++count < mySolutionHandler.Solutions.Count)
            {
                Console.WriteLine();
            }
        }
    }

    private async Task SolveLastDay()
    {
        var lastSolutionDay = mySolutionHandler.Solutions.Keys.LastOrDefault(x => x >= 1 && x <= 25);
        if (lastSolutionDay > 0)
        {
            await SolveDay(lastSolutionDay).ConfigureAwait(false);
        }
        else
        {
            Console.WriteLine("No solution is available yet.");
        }
    }

    private async Task SolveDay(int day)
    {
        var solutionMetadata = mySolutionHandler.Solutions[day];
        var solution = solutionMetadata.CreateInstance();

        var dayString = day.ToString().PadLeft(2, '0');
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var input = await File.ReadAllTextAsync(Path.Combine(rootDir ?? throw new InvalidOperationException("Could not find rootDir."), "Input", $"day{dayString}.txt")).ConfigureAwait(false);

        Console.WriteLine($"Day {day}: {solutionMetadata.Title}");
        await SolvePart(1, input, solution.Part1Async, solution).ConfigureAwait(false);
        await SolvePart(2, input, solution.Part2Async, solution).ConfigureAwait(false);
    }

    private static async Task SolvePart(int partNumber, string input, Func<string, Task<string>> action, ISolution solution)
    {
        var emptyWaitingMessage = $"Calculating Part {partNumber}... ";
        var waitingMessage = emptyWaitingMessage;

        Console.Write(waitingMessage);

        string result;
        try
        {
            solution.ProgressUpdated += ProgressUpdated;
            result = await action(input).ConfigureAwait(false);
        }
        catch (NotImplementedException)
        {
            result = "Not implemented.";
        }
        catch (Exception ex)
        {
            result = $"({ex.GetType().Name}) {ex.Message}";
        }
        finally
        {
            solution.ProgressUpdated -= ProgressUpdated;
        }

        Console.Write($"\r{new string(' ', waitingMessage.Length)}\r");
        Console.Write($"Part {partNumber}: ");
        if (result.Contains(Environment.NewLine))
        {
            result = Environment.NewLine + result;
        }
        Console.WriteLine(result);

        void ProgressUpdated(object _, SolutionProgressEventArgs args)
        {
            if (args.Progress.Percentage > 0)
            {
                var prevLength = waitingMessage.Length;
                waitingMessage = $"\r{emptyWaitingMessage}{Math.Min(99.99, args.Progress.Percentage):0.00}%";
                Console.Write(waitingMessage + new string(' ', Math.Max(0, prevLength - waitingMessage.Length)));
            }
        }
    }

    private async Task SetupDay(int day)
    {
        var dayString = day.ToString().PadLeft(2, '0');
        var consoleProjectBinPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var puzzleProjectPath = Path.Combine(consoleProjectBinPath ?? throw new InvalidOperationException("Could not find consoleProjectBinPath."), myConfig.PuzzleProjectPath);
        Console.WriteLine($"Setting up input and description for {myConfig.Year}/12/{dayString}...");

        var cookieContainer = new CookieContainer();
        cookieContainer.Add(new Cookie("session", myConfig.SessionCookie, "/", "adventofcode.com"));
        using var httpClientHandler = new HttpClientHandler { CookieContainer = cookieContainer };
        using var httpClient = new HttpClient(httpClientHandler);

        await SaveInputAsync(day, dayString, puzzleProjectPath, httpClient).ConfigureAwait(false);
        string puzzleTitle = await SaveDescriptionAsync(day, dayString, puzzleProjectPath, httpClient).ConfigureAwait(false);
        await CreateSolutionSourceAsync(day, dayString, consoleProjectBinPath, puzzleProjectPath, puzzleTitle).ConfigureAwait(false);

        Console.WriteLine("Done.");
    }

    private async Task SaveInputAsync(int day, string dayString, string puzzleProjectPath, HttpClient httpClient)
    {
        var inputAddress = $"https://adventofcode.com/{myConfig.Year}/day/{day}/input";
        var inputFile = new FileInfo(Path.Combine(puzzleProjectPath, "Input", $"day{dayString}.txt"));
        Console.WriteLine($"Downloading input from {inputAddress}");
        var input = await httpClient.GetStringAsync(inputAddress).ConfigureAwait(false);

        Console.WriteLine($"Saving input to {inputFile.FullName}");
        await File.WriteAllTextAsync(inputFile.FullName, input, Encoding.UTF8).ConfigureAwait(false);
    }

    private async Task<string> SaveDescriptionAsync(int day, string dayString, string puzzleProjectPath, HttpClient httpClient)
    {
        var descriptionAddress = $"https://adventofcode.com/{myConfig.Year}/day/{day}";
        var descriptionFile = new FileInfo(Path.Combine(puzzleProjectPath, "Descriptions", $"day{dayString}.html"));
        var puzzleTitleRegex = new Regex("---.*: (?'title'.*) ---");

        Console.WriteLine($"Downloading description from {descriptionAddress}");
        var descriptionPageSource = await httpClient.GetStringAsync(descriptionAddress).ConfigureAwait(false);
        var descriptionPage = new HtmlDocument();
        descriptionPage.LoadHtml(descriptionPageSource);
        var articleNodes = descriptionPage.DocumentNode.SelectNodes("//article[@class='day-desc']");

        var titleNode = articleNodes.First().SelectSingleNode("//h2");
        var puzzleTitle = puzzleTitleRegex.Match(titleNode.InnerText).Groups["title"].Value;
        titleNode.InnerHtml = "--- Part One ---";
        Console.WriteLine($"Found {articleNodes.Count} parts. Title: {puzzleTitle}");
        var description = articleNodes.Aggregate(string.Empty, (result, node) => result + node.OuterHtml);

        Console.WriteLine($"Saving description to {descriptionFile.FullName}");
        await File.WriteAllTextAsync(descriptionFile.FullName, description, Encoding.UTF8).ConfigureAwait(false);

        return puzzleTitle;
    }

    private static async Task CreateSolutionSourceAsync(int day, string dayString, string consoleProjectBinPath, string puzzleProjectPath, string puzzleTitle)
    {
        var solutionSourceFile = new FileInfo(Path.Combine(consoleProjectBinPath, "Template", "Day_DAYSTRING_.cs"));
        var solutionTargetFile = new FileInfo(Path.Combine(puzzleProjectPath, "Solutions", $"Day{dayString}.cs"));
        var testSourceFile = new FileInfo(Path.Combine(consoleProjectBinPath, "Template", "Day_DAYSTRING_Test.cs"));
        var testTargetFile = new FileInfo(Path.Combine($"{puzzleProjectPath}.Test", "Solutions", $"Day{dayString}Test.cs"));

        if (solutionTargetFile.Exists)
        {
            Console.WriteLine($"Source file already exists at {solutionTargetFile.FullName}");
        }
        else
        {
            Console.WriteLine($"Saving source file to {solutionTargetFile.FullName}");
            var sourceContent = await File.ReadAllTextAsync(solutionSourceFile.FullName).ConfigureAwait(false);
            sourceContent = sourceContent
                .Replace("_DAYNUMBER_", day.ToString())
                .Replace("_DAYSTRING_", dayString)
                .Replace("_PUZZLETITLE_", puzzleTitle);
            await File.WriteAllTextAsync(solutionTargetFile.FullName, sourceContent, Encoding.UTF8).ConfigureAwait(false);
        }

        if (testTargetFile.Exists)
        {
            Console.WriteLine($"Test file already exists at {solutionTargetFile.FullName}");
        }
        else
        {
            Console.WriteLine($"Saving test file to {testTargetFile.FullName}");
            var testContent = await File.ReadAllTextAsync(testSourceFile.FullName).ConfigureAwait(false);
            testContent = testContent.Replace("_DAYSTRING_", dayString);
            await File.WriteAllTextAsync(testTargetFile.FullName, testContent, Encoding.UTF8).ConfigureAwait(false);
        }
    }

    private readonly Options myOptions;
    private readonly Configuration myConfig;
    private readonly SolutionHandler mySolutionHandler;
}