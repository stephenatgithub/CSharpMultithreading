using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp
{
    public class CancellationTokenTest
    {
        public CancellationTokenSource CTS = new();

        public void test()
        {
            CTS.CancelAfter(TimeSpan.FromSeconds(5));

            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3,
                //Set the CancellationToken value
                CancellationToken = CTS.Token
            };

            try
            {
                // loop for 20 times
                Parallel.For(1, 21, parallelOptions, i => {
                    // delay 1 sec
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine($"Value of i = {i}, thread = {Thread.CurrentThread.ManagedThreadId}");
                });
            }
            //When the token canceled, it will throw an exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Finally dispose the CancellationTokenSource and set its value to null
                CTS.Dispose();
                CTS = null;
            }
        }
    }
}
