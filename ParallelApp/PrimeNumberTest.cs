using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp
{
    public  class PrimeNumberTest
    {

        // 2 million
        public static int limit = 2_000_000;
        public IList<int> numbers = Enumerable.Range(0, limit).ToList();

        //var primeNumbersFromForeach = GetPrimeList(numbers);

        //var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);

        [Benchmark]
        public void NormalGetPrimeList()
        {
            GetPrimeList(numbers);
        }


        [Benchmark]
        public void ParallelGetPrimeList()
        {
            GetPrimeListWithParallel(numbers);
        }


        /// <summary>
        /// GetPrimeList returns Prime numbers by using sequential ForEach
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private static IList<int> GetPrimeList(IList<int> numbers) => numbers.Where(IsPrime).ToList();

        /// <summary>
        /// GetPrimeListWithParallel returns Prime numbers by using Parallel.ForEach
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });

            return primeNumbers.ToList();
        }

        /// <summary>
        /// IsPrime returns true if number is Prime, else false.(https://en.wikipedia.org/wiki/Prime_number)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
