using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp
{
    public class ParallelTest
    {
        [Benchmark]
        public int[] NormalForEach()
        {
            var array = new int[1_000_000];

            for (var i=0; i < 1_000_000; i++)
            {
                array[i] = i;
            }

            return array;
        }

        [Benchmark]
        public int[] ParallelForEach()
        {
            var array = new int[1_000_000];

            Parallel.For (0, 1_000_000, i =>
            {
                array[i] = i;
            });

            return array;
        }


    }
}
