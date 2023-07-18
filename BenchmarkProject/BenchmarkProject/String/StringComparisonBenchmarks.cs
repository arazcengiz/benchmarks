using BenchmarkDotNet.Attributes;
using BenchmarkProject.Utilities;
using System.Text;

namespace BenchmarkProject.String;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
//[BenchmarkDotNet.Attributes.MinColumn]
[AllStatisticsColumn]
[GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByParams)]
[RankColumn]
[BenchmarkClass]
public class StringComparisonBenchmarks
{
    private List<string> _dataList { get; set; }
    private int _dataListSize { get; set; }

    [Params(1000, 10_000, 100_000)]
    public int _listLength { get; set; }

    private string _compareText { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Console.WriteLine($"Setup Started with {_listLength}");

        var rnd = new Random();
        const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";

        _dataList = new List<string>();
        for (int i = 0; i < _listLength; i++)
        {
            int stringLength = rnd.Next(allowedChars.Length);
            string subStr = allowedChars.Substring(0, stringLength);
            _dataList.Add(subStr);
        }

        Console.WriteLine($"Setup Finished with {_listLength}");

        _dataListSize = _dataList.Sum(item => item.Length);
        Console.WriteLine($"Setup DataListSize: {_dataListSize}");

        _compareText = allowedChars.Substring(0, rnd.Next(allowedChars.Length));
    }

    //[Benchmark(Baseline = true)]
    [Benchmark]
    public bool CompareByOperator()
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i] == _compareText) return true;
        }
        return false;
    }

    [Benchmark]
    public bool CompareByOperatorWithCase()
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i].ToLower() == _compareText.ToLower()) return true;
        }
        return false;
    }

    [Benchmark]
    public bool CompareByEquals()
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i].Equals(_compareText)) return true;
        }
        return false;
    }

    [Benchmark]
    public bool CompareByEqualsWithIgnoreCase()
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i].Equals(_compareText, StringComparison.InvariantCultureIgnoreCase)) return true;
        }
        return false;
    }
}
