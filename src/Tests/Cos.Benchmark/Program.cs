using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Easee.IoT.DataTypes.Observations;
using System;
using System.Collections.Generic;
using BenchmarkDotNet.Jobs;

namespace Easee.Cos.Benchmark
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [MemoryDiagnoser]
    public class CosDeserialize
    {
        private readonly CosReader _cosReader;
        readonly byte[] _oneObservation = Convert.FromBase64String("ASAAAWCVLGMAATB4QS1gQg==");
        readonly byte[] _tenObservations = Convert.FromBase64String("ASAAAWCVK4wACjB4AAAAADC2PffO2TC3PffO2TDKQ10t0zDLQ1y0OTDMQ1Z9sjDNQ1bcKUBtAAAABMBkAAFCQC4AAAAX");

        public CosDeserialize()
        {
            _cosReader = new();
        }

        [Benchmark]
        public List<Observation> DeserializePackageWithOneObservation() =>
            _cosReader.Deserialize(_oneObservation);

        [Benchmark]
        public List<Observation> DeserializePackageWithTenObservations() =>
            _cosReader.Deserialize(_tenObservations);
    }

    /// <summary>
    /// Run this to get a benchmark of the performance (run time and memory allocations)
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CosDeserialize>();
        }
    }
}
