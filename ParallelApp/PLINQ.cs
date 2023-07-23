using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp
{
    public class PLINQ
    {
        public IEnumerable<int> numbers = Enumerable.Range(1, 10_000);

        [Benchmark]
        public void NormalEvenNumbers()
        {
            var normalEvenNumbers = numbers.Where(x => x % 2 == 0).ToList();
        }

        [Benchmark]
        public void ParallelEvenNumbers()
        {
            var parallelEvenNumbers = numbers
                .AsParallel() //Parallel Processing
                .AsOrdered() //Original Order of the numbers
                .Where(x => x % 2 == 0)
                .ToList();
        }

        [Benchmark]
        public void NormalSum()
        {
            var normalSum = numbers.Sum();
        }

        [Benchmark]
        public void ParallelSum()
        {
            var parallelSum = numbers.AsParallel().Sum();
        }

        [Benchmark]
        public void NormalAverage()
        {
            var normalAverage = numbers.Average();
        }

        [Benchmark]
        public void ParallelAverage()
        {
            var parallelAverage = numbers.AsParallel().Average();
        }



    }
}
