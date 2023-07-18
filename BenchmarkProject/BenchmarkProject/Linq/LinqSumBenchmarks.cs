using BenchmarkDotNet.Attributes;
using BenchmarkProject.Utilities;
using System.Text;

namespace BenchmarkProject.Linq;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
//[BenchmarkDotNet.Attributes.MinColumn]
[AllStatisticsColumn]
[GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByParams)]
[RankColumn]
[BenchmarkClass]
public class LinqSumBenchmarks
{
    private List<string> _dataList { get; set; }
    private string[] _dataArray { get; set; }
    private Span<string> _dataSpan => _dataArray;

    [Params(1000, 10_000, 100_000, 1_000_000)]
    public int _listLength { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Console.WriteLine($"Setup Started with {_listLength}");

        var rndStringLength = new Random();
        const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";

        _dataList = new List<string>();
        for (int i = 0; i < _listLength; i++)
        {
            int stringLength = rndStringLength.Next(allowedChars.Length);
            string subStr = allowedChars.Substring(0, stringLength);
            _dataList.Add(subStr);
        }

        _dataArray = _dataList.ToArray();

        Console.WriteLine($"Setup Finished with {_listLength}");
    }

    //[Benchmark(Baseline = true)]
    [Benchmark]
    public int ListSelectThenSum()
    {
        return _dataList.Select(item => item.Length).Sum();
    }

    [Benchmark]
    public int ListSum()
    {
        return _dataList.Sum(item => item.Length);
    }

    [Benchmark]
    public int ArraySelectThenSum()
    {
        return _dataArray.Select(item => item.Length).Sum();
    }

    [Benchmark]
    public int ArraySum()
    {
        return _dataArray.Sum(item => item.Length);
    }

    [Benchmark]
    public int SpanForLoopAddition()
    {
        int sum = 0;
        for (int i = 0; i < _dataSpan.Length; i++)
        {
            sum += _dataSpan[i].Length;
        }
        return sum;
    }
}
