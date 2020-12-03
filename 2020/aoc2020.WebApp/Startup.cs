using aoc2020.Puzzles.Core;
using aoc2020.WebApp.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace aoc2020.WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISolutionHandler, SolutionHandler>();
            services.AddSingleton<IInputHandler, InputHandler>();
            services.AddSingleton<IVisualizerHandler, VisualizerHandler>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
