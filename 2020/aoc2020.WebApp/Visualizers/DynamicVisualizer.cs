using aoc2020.Puzzles.Core;
using aoc2020.WebApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace aoc2020.WebApp.Visualizers
{
    public sealed class DynamicVisualizer : ComponentBase
    {
        [Parameter]
        public ISolution SolutionInstance { get; set; }

        [Inject]
        public IVisualizerHandler VisualizerHandler { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (SolutionInstance == null) { return; }
            var visualizerType = VisualizerHandler.GetVisualizer(SolutionInstance.GetType());
            if (visualizerType == null) { return; }

            builder.OpenComponent(0, visualizerType);
            builder.AddAttribute(1, "SolutionInstance", SolutionInstance);
            builder.CloseComponent();
        }
    }
}
