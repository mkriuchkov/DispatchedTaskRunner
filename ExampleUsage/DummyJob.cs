using System;
using System.Threading.Tasks;
using DispatchedTasksRunner;

namespace ExampleUsage
{
    public class DummyJob : ICalledJob<object, string>
    {
        public async Task<string> Run(object? input)
        {
            var random = new Random();
            var executionTime = random.Next(500, 2000);
            Console.WriteLine($"Executing {input} task");
            await Task.Delay(executionTime);
            return $"{input} task returned result in {executionTime} milliseconds";
        }
    }
}