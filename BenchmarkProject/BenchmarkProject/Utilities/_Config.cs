using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkProject.Utilities
{
    internal class _Config : ManualConfig
    {
        public _Config()
        {
            base.SummaryStyle = SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Percentage);
        }
    }
}
