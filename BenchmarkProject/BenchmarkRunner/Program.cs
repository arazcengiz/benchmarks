using System;
using System.Reflection;
using BenchmarkDotNet.Running;
using BenchmarkProject.String;

internal class Program
{
    private static void Main(string[] args)
    {
        var benchmarkClasses = BenchmarkProject.Utilities.ReflectionHelper.GetBenchmarkAssemblies();
        foreach (var benchmarkClass in benchmarkClasses)
        {
            Console.WriteLine(benchmarkClass.Key);
        }
        Console.WriteLine("Select a class to run benchmarks");

        string? chosenItem = Console.ReadLine();

        if (!string.IsNullOrEmpty(chosenItem) && benchmarkClasses.ContainsKey(chosenItem)) 
        {
            Console.WriteLine($"Running benchmark for {chosenItem}");
            var summary = BenchmarkRunner.Run(benchmarkClasses[chosenItem]);
            Console.WriteLine($"Finished running benchmark for {chosenItem}");
        }
    }
}