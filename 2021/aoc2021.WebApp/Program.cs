﻿using aoc2021.Puzzles.Core;
using aoc2021.WebApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace aoc2021.WebApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync().ConfigureAwait(false);
    }

    public static WebAssemblyHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault();
        builder.RootComponents.Add<App>("#app");
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddSingleton<ISolutionHandler, SolutionHandler>();
        builder.Services.AddSingleton<IInputHandler, InputHandler>();
        builder.Services.AddSingleton<IVisualizerHandler, VisualizerHandler>();
        return builder;
    }
}