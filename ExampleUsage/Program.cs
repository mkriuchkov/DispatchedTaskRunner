using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DispatchedTasksRunner;

namespace ExampleUsage
{
    internal static class Program
    {
        private const int MaxParallelJobsCount = 5;

        public static async Task Main(string[] args)
        {
            var dummyJob = new DummyJob();
            var jobs = new List<Func<Task<string>>>();

            // Create list of jobs to be executed in future. For example let it be 53 jobs 
            for (var i = 0; i < 53; i++)
            {
                // i is being modified in the outer scope so it needs to be copied to 
                // provide proper input parameter to each job
                var iteratorCopy = i;

                // Each job can have one input parameter
                jobs.Add(async () => await dummyJob.Run(iteratorCopy));
            }

            var dispatcher = new TasksDispatcher<string>(jobs, MaxParallelJobsCount);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Report: ....");
            await foreach (var jobResult in dispatcher.RunAll())
            {
                Console.WriteLine(jobResult);
            }
        }
    }
}