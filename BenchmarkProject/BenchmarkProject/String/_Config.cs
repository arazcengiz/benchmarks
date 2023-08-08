using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace BenchmarkProject.String
{
    internal class _Config : ManualConfig
    {
        public _Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Percentage);
        }
    }
}
