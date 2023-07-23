# Multithread

Go to debug mode, under Debug, Windows, Open Theads, Parallel Stacks



# Benchmark

- BenchmarkDotNet
- run in release mode

# Comparsion for Parallel

|          Method |     Mean |     Error |    StdDev |
|---------------- |---------:|----------:|----------:|
|   NormalForEach | 1.226 ms | 0.0204 ms | 0.0346 ms |
| ParallelForEach | 1.032 ms | 0.0206 ms | 0.0332 ms |

|               Method |      Mean |     Error |   StdDev |    Median |
|--------------------- |----------:|----------:|---------:|----------:|
|   NormalGetPrimeList | 411.28 ms | 17.288 ms | 50.97 ms | 418.66 ms |
| ParallelGetPrimeList |  58.24 ms |  5.902 ms | 17.40 ms |  70.66 ms |


- Iterations are independent of each other, and not in sequential order.
- Do Not Assume That Parallel Is Always Faster
- Store thread-local state during loop execution


# Race condition

Use thread safe methods

|       Method |     Mean |    Error |   StdDev |
|------------- |---------:|---------:|---------:|
|      UseLock | 39.11 ms | 0.435 ms | 0.407 ms |
| UseInterLock | 17.59 ms | 0.040 ms | 0.039 ms |

- Interlock
- Lock


# Parallel LINQ

```
// using PLINQ
var evenNumbers = numbers
	.AsParallel() //Parallel Processing
	.AsOrdered() //Original Order of the numbers
	.WithDegreeOfParallelism(2) //Maximum of two threads can process the data
	.WithCancellation(CTS.Token) //Cancel the operation after 200 Milliseconds
	.Where(x => x % 2 == 0) //This logic will execute in parallel
	.ToList();
```

|              Method |      Mean |    Error |    StdDev |    Median |
|-------------------- |----------:|---------:|----------:|----------:|
|   NormalEvenNumbers |  52.99 us | 2.269 us |  6.692 us |  48.53 us |
| ParallelEvenNumbers | 293.27 us | 5.639 us |  7.132 us | 294.87 us |
|           NormalSum |  28.80 us | 0.087 us |  0.073 us |  28.81 us |
|         ParallelSum | 155.91 us | 1.760 us |  1.374 us | 155.93 us |
|       NormalAverage |  32.79 us | 0.975 us |  2.874 us |  33.42 us |
|     ParallelAverage | 230.66 us | 8.033 us | 21.855 us | 236.70 us |


# Cancellation Token

`
public CancellationTokenSource CTS = new();

CTS.CancelAfter(TimeSpan.FromSeconds(5));

var parallelOptions = new ParallelOptions()
{
	MaxDegreeOfParallelism = 3,
	//Set the CancellationToken value
	CancellationToken = CTS.Token
};
`