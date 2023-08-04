using BenchmarkDotNet.Attributes;
using BenchmarkProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkProject.Linq
{
    /// <summary>
    /// The idea of this benchmark is to compare Linq.Any() method with Exists() method
    /// </summary>
    ///
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    //[BenchmarkDotNet.Attributes.MinColumn]
    //[AllStatisticsColumn]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByParams)]
    [RankColumn]
    [BaselineColumn]
    [BenchmarkClass]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net48, baseline:true)]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp30)]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net60)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net70)]
    public class LinqAnyBenchmarks
    {
        private List<string> _dataList { get; set; }
        private string[] _dataArray { get; set; }
        private Span<string> _dataSpan => _dataArray;

        [Params(100, 1000, 10000, 100_000, 1_000_000)]
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

            _dataArray = _dataList.ToArray();

            Console.WriteLine($"Setup Finished with {_listLength}");

            _compareText = allowedChars.Substring(0, rnd.Next(allowedChars.Length));
        }

        [Benchmark(Baseline = true)]
        public bool ListWithExists()
        {
            return _dataList.Exists(item => item == _compareText);
        }

        [Benchmark]
        public bool ListWithAny()
        {
            return _dataList.Any(item => item == _compareText);
        }

        [Benchmark]
        public bool SpanWithIndexOf()
        {
            return _dataSpan.IndexOf(_compareText) >= 0;
        }
    }
}