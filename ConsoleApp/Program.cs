
Task task = new Task(() =>
{
    Console.WriteLine("Running task in separate thread...");

    int result = AddNumber(5, 10);

    Console.WriteLine("result = {0}", result);
});

task.Start();


Console.WriteLine("Main thread!");

Console.ReadLine();




int AddNumber(int a, int b) => a + b;