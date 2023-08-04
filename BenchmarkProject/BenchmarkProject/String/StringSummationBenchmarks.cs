using BenchmarkDotNet.Attributes;
using BenchmarkProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkProject.String
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    //[BenchmarkDotNet.Attributes.MinColumn]
    [AllStatisticsColumn]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByParams)]
    [RankColumn]
    [BenchmarkClass]
    public class StringSummationBenchmarks
    {
        private List<string> _dataList { get; set; }
        private int _dataListSize { get; set; }

        [Params(1000)]
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

            Console.WriteLine($"Setup Finished with {_listLength}");

            _dataListSize = _dataList.Sum(item => item.Length);
            Console.WriteLine($"Setup DataListSize: {_dataListSize}");
        }

        //[Benchmark(Baseline = true)]
        [Benchmark]
        public string ConcatenationFor()
        {
            string str = string.Empty;
            for (int i = 0; i < _dataList.Count; i++)
            {
                str += _dataList[i];
            }
            return str;
        }

        [Benchmark]
        public string ConcatenationForParallel()
        {
            string str = string.Empty;
            Parallel.For(0, _dataList.Count, item => str += _dataList[item]);
            return str;
        }

        [Benchmark]
        public string ConcatenationForeach()
        {
            string str = string.Empty;
            foreach (string item in _dataList)
            {
                str += item;
            }
            return str;
        }

        [Benchmark]
        public string StringBuilder()
        {
            var str = new StringBuilder();
            foreach (string item in _dataList)
            {
                str.Append(item);
            }
            return str.ToString();
        }

        [Benchmark]
        public string StringBuilderParallel()
        {
            var str = new StringBuilder();
            Parallel.ForEach(_dataList, item => str.Append(item));
            return str.ToString();
        }

        [Benchmark]
        public string StringBuilderExact()
        {
            var str = new StringBuilder(_dataListSize);
            foreach (string item in _dataList)
            {
                str.Append(item);
            }
            return str.ToString();
        }

        [Benchmark]
        public string StringJoin()
        {
            var str = string.Join("", _dataList);
            return str;
        }
    }
}