using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp
{
    public class RaceConditionTest
    {
        public static readonly object lockObject = new object();
        public static int IncrementValue = 0;
        public const int NumberOfIteration = 1_000_000;

        [Benchmark]
        public void UseLock()
        {
            Parallel.For(0, NumberOfIteration, number =>
            {
                lock (lockObject)
                {
                    IncrementValue++;
                }
            });

        }

        [Benchmark]
        public void UseInterLock()
        {
            Parallel.For(0, NumberOfIteration, number =>
            {
                Interlocked.Increment(ref IncrementValue);
            });
        }
    }
}
