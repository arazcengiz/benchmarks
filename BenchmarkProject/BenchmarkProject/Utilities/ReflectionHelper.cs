using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BenchmarkProject.Utilities
{
    public static class ReflectionHelper
    {
        public static Dictionary<string, Type> GetBenchmarkAssemblies()
        {
            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            var benchmarkClasses = assemblyTypes.Where(item => item.GetCustomAttribute<BenchmarkClassAttribute>() != null).ToList();

            Dictionary<string, Type> result = new Dictionary<string, Type>();
            benchmarkClasses.ForEach(item => result.Add(item.Name, item));
            return result;
        }
    }
}