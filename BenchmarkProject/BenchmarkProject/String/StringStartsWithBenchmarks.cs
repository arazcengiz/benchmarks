using BenchmarkDotNet.Attributes;
using BenchmarkProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkProject.String
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    //[BenchmarkDotNet.Attributes.MinColumn]
    [AllStatisticsColumn]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByParams)]
    [RankColumn]
    [BaselineColumn]
    [BenchmarkClass]
    public class StringStartsWithBenchmarks
    {
        private List<string> _dataList { get; set; }

        [Params(10_000)]
        public int _listLength { get; set; }

        private string _compareText { get; set; }
        private ReadOnlySpan<char> _compareTextSpan => _compareText.AsSpan();

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

            _compareText = allowedChars.Substring(0, rnd.Next(allowedChars.Length));
        }

        [Arguments("http://www.google.com")]
        [Benchmark(Baseline = true)]
        public bool StartsWith()
        {
            for (int i = 0; i < _dataList.Count; i++)
            {
                if (_dataList[i].StartsWith(_compareText)) return true;
            }
            return false;
        }

        [Benchmark]
        public bool StartsWithSpanBoth()
        {
            for (int i = 0; i < _dataList.Count; i++)
            {
                if (_dataList[i].AsSpan().StartsWith(_compareText.AsSpan())) return true;
            }
            return false;
        }

        [Benchmark]
        public bool StartsWithSpanList()
        {
            for (int i = 0; i < _dataList.Count; i++)
            {
                if (_dataList[i].AsSpan().StartsWith(_compareTextSpan)) return true;
            }
            return false;
        }
    }
}