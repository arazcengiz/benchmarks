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
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByJob)]
    [RankColumn]
    [BaselineColumn]
    [BenchmarkClass]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net48, baseline:true)]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp30)]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net60)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net70)]
    [Config(typeof(_Config))]
    public class LinqCountBenchmarks
    {
        private List<char> _dataList { get; set; }
        private char[] _dataArray { get; set; }
        private Span<char> _dataSpan => _dataArray;

        [GlobalSetup]
        public void Setup()
        {
            var rnd = new Random();
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";

            int length = rnd.Next(allowedChars.Length);

            _dataList = allowedChars.Substring(0, length).ToList();
            _dataArray = _dataList.ToArray();

            Console.WriteLine($"Setup Finished with {length}");
        }

        [Benchmark(Baseline = true)]
        public int ListCountProperty()
        {
            return _dataList.Count;
        }

        [Benchmark]
        public int ArrayLengthProperty()
        {
            return _dataArray.Length;
        }

        [Benchmark]
        public int SpanLengthProperty()
        {
            return _dataSpan.Length;
        }

        [Benchmark]
        public int EnumerableCountMethod()
        {
            return _dataList.Count();
        }
    }
}