using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkProject.String
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [Config(typeof(_Config))]
    [BenchmarkClass]
    
    public class StringComparisonNoListBenchmarks
    {
        private string _compareText1 { get; set; }
        private string _compareText2 { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            const string allowedChars1 = "AbCdEfGhIjKlMnOpQrStUvWxYz";
            const string allowedChars2 = "aBcDeFgHiJkLmNoPqRsTuVwXyZ";
         
            var rnd = new Random();
            int length = rnd.Next(allowedChars1.Length);

            _compareText1 = allowedChars1.Substring(0, length);
            _compareText2 = allowedChars2.Substring(0, length);

            Console.WriteLine($"Text1: {_compareText1}");
            Console.WriteLine($"Text2: {_compareText2}");
        }

        [Benchmark(Baseline = true)]
        public bool CompareByOperator() => _compareText1 == _compareText2;

        [Benchmark]
        public bool CompareByOperatorWithToLower() => _compareText1.ToLower() == _compareText2.ToLower();

        [Benchmark]
        public bool CompareByOperatorWithToUpper() => _compareText1.ToUpper() == _compareText2.ToUpper();

        [Benchmark]
        public bool CompareByEquals() => _compareText1.Equals(_compareText2);

        [Benchmark]
        public bool CompareByEqualsWithIgnoreCase() => _compareText1.Equals(_compareText2, StringComparison.InvariantCultureIgnoreCase);
    }
}