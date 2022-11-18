﻿using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace aoc2022.ConsoleApp;

public sealed class Configuration
{
    public int Year { get; set; } = 2022;

    public string PuzzleProjectPath { get; set; } = Path.Combine("..", "..", "..", "..", "aoc2022.Puzzles");

    public string SessionCookie { get; set; }

    private Configuration() { }

    public static Configuration Load()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true, true)
            .Build();

        var configuration = new Configuration();
        config.Bind(configuration);

        return configuration;
    }
}